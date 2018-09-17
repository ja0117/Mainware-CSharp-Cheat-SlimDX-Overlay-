
using SharpDX.Direct2D1;
using SharpDX.DirectWrite;
using SharpDX.Mathematics.Interop;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Factory = SharpDX.Direct2D1.Factory;
using FontFactory = SharpDX.DirectWrite.Factory;
using SharpDX.Direct3D11;
using Mainware_Full.Offset_things;
using Mainware_Full.Models;
using Mainware_Full.Security;




/**
 * Coded by JoshuaA. Any questions? Contact me on discord: @JoshuaA#6951 
 * Coded by JoshuaA. Any questions? Contact me on discord: @JoshuaA#6951
 * Coded by JoshuaA. Any questions? Contact me on discord: @JoshuaA#6951
 * Coded by JoshuaA. Any questions? Contact me on discord: @JoshuaA#6951
 * Coded by JoshuaA. Any questions? Contact me on discord: @JoshuaA#6951
 * Coded by JoshuaA. Any questions? Contact me on discord: @JoshuaA#6951
 * Coded by JoshuaA. Any questions? Contact me on discord: @JoshuaA#6951
 * Coded by JoshuaA. Any questions? Contact me on discord: @JoshuaA#6951
 * Coded by JoshuaA. Any questions? Contact me on discord: @JoshuaA#6951
 * Coded by JoshuaA. Any questions? Contact me on discord: @JoshuaA#6951
 * Coded by JoshuaA. Any questions? Contact me on discord: @JoshuaA#6951
 * Coded by JoshuaA. Any questions? Contact me on discord: @JoshuaA#6951
 * Coded by JoshuaA. Any questions? Contact me on discord: @JoshuaA#6951
 * Coded by JoshuaA. Any questions? Contact me on discord: @JoshuaA#6951
 * Coded by JoshuaA. Any questions? Contact me on discord: @JoshuaA#6951
 */


namespace Mainware_Full
{
    public partial class FrmSharpDX : Form
    {

        private WindowRenderTarget _device;
        private HwndRenderTargetProperties _renderProperties;
        private SolidColorBrush _solidColorBrush;
        private Factory _factory;

        private SolidColorBrush _boxBrush, _lifeBrush, _lineBrush;

        // Fonts
        private TextFormat _font, _fontSmall;
        private FontFactory _fontFactory;
        private const string _fontFamily = "PROFONT";
        private const float _fontSize = 12.0f;
        private const float _fontSizeSmall = 10.0f;

        private IntPtr _handle;
        private Thread _threadDx = null;

        private float[] _viewMatrix = new float[16];
        private Vector3 _worldToScreenPos = new Vector3();

        private bool _running = false;

        public FrmSharpDX()
        {
            _handle = Handle;
            InitializeComponent();
        }


        RECT rect;
        public const string WINDOW_NAME = "Counter-Strike: Global Offensive";
        
        IntPtr handle = FindWindow(null, WINDOW_NAME);

        public struct RECT
        {
            public int left, top, right, bottom;
        }


        Graphics g;
        Pen myPen = new Pen(Color.Red);


        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);




        private void FrmSharpDX_Load(object sender, EventArgs e)
        {
            //this.BackColor = Color.Wheat;
            this.TransparencyKey = System.Drawing.Color.Black;
            this.TopMost = true;

            long initialStyle = GetWindowLong(this.handle, -20);
            SetWindowLong(this.Handle, -20, GetWindowLong(this.Handle, -20) | 0x00000020);

            GetWindowRect(handle, out rect);
            this.Size = new Size(rect.right - rect.left, rect.bottom - rect.top);
            this.Top = rect.top;
            this.Left = rect.left;

            DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint |
                ControlStyles.Opaque | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);

            _factory = new Factory();
            _fontFactory = new FontFactory();
            _renderProperties = new HwndRenderTargetProperties
            {
                Hwnd = Handle,
                PixelSize = new SharpDX.Size2(Size.Width, Size.Height),
                PresentOptions = PresentOptions.Immediately
            };

            // Initialize DirectX
            _device = new WindowRenderTarget(_factory, new RenderTargetProperties(new PixelFormat(SharpDX.DXGI.Format.B8G8R8A8_UNorm, AlphaMode.Premultiplied)), _renderProperties);
            _solidColorBrush = new SolidColorBrush(_device, new RawColor4(Color.White.R, Color.White.G, Color.White.B, Color.White.A));

            _boxBrush = new SolidColorBrush(_device, new RawColor4(255, 0, 0, 1f));

            _lifeBrush = new SolidColorBrush(_device, new RawColor4(0, 255, 0, 1f));

            _lineBrush = new SolidColorBrush(_device, new RawColor4(150, 150, 0, 5f));

            // Initialize Fonts
            _font = new TextFormat(_fontFactory, _fontFamily, _fontSize);
            _fontSmall = new TextFormat(_fontFactory, _fontFamily, _fontSizeSmall);

            _threadDx = new Thread(new ParameterizedThreadStart(DirectXThread))
            {
                Priority = ThreadPriority.Highest,
                IsBackground = true
            };

            _running = true;
            _threadDx.Start();

        }

        private void DirectXThread(object sender)
        {
            List<PlayerModel> currentPlayerList = new List<PlayerModel>();

            float posX = 0;
            float posY = 0;

            float headX = 0;
            float headY = 0;

            float height = 0;
            float width = 0;

            while (_running)
            {
                _device.BeginDraw();
                _device.Clear(new RawColor4(Color.Transparent.R, Color.Transparent.G, Color.Transparent.B, Color.Transparent.A));
                _device.TextAntialiasMode = SharpDX.Direct2D1.TextAntialiasMode.Aliased;


                // Render our ESP here
                currentPlayerList = Models.Players.GetPlayer();

                Int32 localPlayer = Memory.Read<Int32>(GameCheck.bClient + signatures.dwLocalPlayer);
                Int32 localTeam = Memory.Read<Int32>(localPlayer + netvars.m_iTeamNum);

                for (int i = 0; i < 16; i++)
                    _viewMatrix[i] = Memory.Read<float>(GameCheck.bClient + signatures.dwViewMatrix + (i * 0x4));

                foreach (var player in currentPlayerList)
                {
                    Vector3 entityPosition = new Vector3
                    {
                        X = player.PosX,
                        Y = player.PosY,
                        Z = player.PosZ
                    };

                    Vector3 booleanVector = new Vector3();

                    if (WorldToScreen(entityPosition, booleanVector))
                    {
                        posX = _worldToScreenPos.X;
                        posY = _worldToScreenPos.Y;

                        Vector3 entityHeadPosition = new Vector3
                        {
                            X = player.HeadX,
                            Y = player.HeadY,
                            Z = player.HeadZ
                        };

                        if (WorldToScreen(entityHeadPosition, booleanVector))
                        {
                            headX = _worldToScreenPos.X;
                            headY = _worldToScreenPos.Y - _worldToScreenPos.Y / 64;

                            if (player.Team != localTeam && !player.Dormant)
                            {
                                if (player.Health > 0)
                                {
                                    height = posY - headY;
                                    width = height / 2;

                                    float[] a = new float[2];
                                    a[0] = 1;
                                    a[1] = 2;

                                    _device.DrawLine(new RawVector2(posX - width / 2, posY), new RawVector2(posX - width / 2, headY), _boxBrush, 2f); // Left
                                    _device.DrawLine(new RawVector2(posX - width / 2, headY), new RawVector2(headX + width / 2, headY), _boxBrush, 2f); // Top
                                    _device.DrawLine(new RawVector2(headX + width / 2, headY), new RawVector2(posX + width / 2, posY), _boxBrush, 2f); // Right
                                    _device.DrawLine(new RawVector2(posX + width / 2, posY), new RawVector2(posX - width / 2, posY), _boxBrush, 2f); // Bottom

                                    _device.DrawLine(new RawVector2(posX - width / 2, posY), new RawVector2(posX - width / 2, headY), _lifeBrush, 2f); // Health

                                    _device.DrawLine(new RawVector2(1920/2, 1500), new RawVector2(posX - width / 100, posY), _lineBrush, 0.5f); // Line
                                }
                            }
                        }
                    }
                }

                // End Render of our ESP

                _device.EndDraw();
            }
        }

        bool WorldToScreen(Vector3 from, Vector3 to)
        {
            float w = 0.0f;

            to.X = _viewMatrix[0] * from.X + _viewMatrix[1] * from.Y + _viewMatrix[2] * from.Z + _viewMatrix[3];
            to.Y = _viewMatrix[4] * from.X + _viewMatrix[5] * from.Y + _viewMatrix[6] * from.Z + _viewMatrix[7];

            w = _viewMatrix[12] * from.X + _viewMatrix[13] * from.Y + _viewMatrix[14] * from.Z + _viewMatrix[15];

            if (w < 0.01f)
                return false;

            to.X *= (1.0f / w);
            to.Y *= (1.0f / w);

            int width = Size.Width;
            int height = Size.Height;

            float x = width / 2;
            float y = height / 2;

            x += 0.5f * to.X * width + 0.5f;
            y -= 0.5f * to.Y * height + 0.5f;

            to.X = x;
            to.Y = y;

            _worldToScreenPos.X = to.X;
            _worldToScreenPos.Y = to.Y;

            return true;
        }

        //private void FrmSharpDX_Paint(object sender, PaintEventArgs e)
        //{
        //    g = e.Graphics;
        //    g.DrawRectangle(myPen, 100, 100, 200, 200);
        //}

    }
}
/**
 * Coded by JoshuaA. Any questions? Contact me on discord: @JoshuaA#6951 
 * Coded by JoshuaA. Any questions? Contact me on discord: @JoshuaA#6951
 * Coded by JoshuaA. Any questions? Contact me on discord: @JoshuaA#6951
 * Coded by JoshuaA. Any questions? Contact me on discord: @JoshuaA#6951
 * Coded by JoshuaA. Any questions? Contact me on discord: @JoshuaA#6951
 * Coded by JoshuaA. Any questions? Contact me on discord: @JoshuaA#6951
 * Coded by JoshuaA. Any questions? Contact me on discord: @JoshuaA#6951
 * Coded by JoshuaA. Any questions? Contact me on discord: @JoshuaA#6951
 * Coded by JoshuaA. Any questions? Contact me on discord: @JoshuaA#6951
 * Coded by JoshuaA. Any questions? Contact me on discord: @JoshuaA#6951
 * Coded by JoshuaA. Any questions? Contact me on discord: @JoshuaA#6951
 * Coded by JoshuaA. Any questions? Contact me on discord: @JoshuaA#6951
 * Coded by JoshuaA. Any questions? Contact me on discord: @JoshuaA#6951
 * Coded by JoshuaA. Any questions? Contact me on discord: @JoshuaA#6951
 * Coded by JoshuaA. Any questions? Contact me on discord: @JoshuaA#6951
 */
