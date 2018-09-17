using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mainware_Full.Security;

namespace Mainware_Full.Models
{
    class Players
    {
        public static List<PlayerModel> GetPlayer()
        {
            List<PlayerModel> playerList = new List<PlayerModel>();

            for (int i = 0; i < 64; i++)
            {
                Int32 playerBase = Memory.Read<Int32>(GameCheck.bClient + signatures.dwEntityList + (i * 0x10));
                Int32 localPlayerBase = Memory.Read<Int32>(GameCheck.bClient + signatures.dwEntityList);

                if (playerBase == 0)
                    continue;

                float playerX = Memory.Read<float>(playerBase + netvars.m_vecOrigin + (0 * 0x4));
                float playerY = Memory.Read<float>(playerBase + netvars.m_vecOrigin + (1 * 0x4));
                float playerZ = Memory.Read<float>(playerBase + netvars.m_vecOrigin + (2 * 0x4));

                float playerHeadX = playerX + Memory.Read<float>(playerBase + netvars.m_vecViewOffset + (0 * 0x4));
                float playerHeadY = playerY + Memory.Read<float>(playerBase + netvars.m_vecViewOffset + (1 * 0x4));
                float playerHeadZ = playerZ + Memory.Read<float>(playerBase + netvars.m_vecViewOffset + (2 * 0x4));

                Int32 playerHealth = Memory.Read<Int32>(playerBase + netvars.m_iHealth);

                Int32 playerTeam = Memory.Read<Int32>(playerBase + netvars.m_iTeamNum);

                bool playerDormant = Memory.Read<bool>(playerBase + 0xE9);

                PlayerModel currentPlayer = new PlayerModel
                {
                    PosX = playerX,
                    PosY = playerY,
                    PosZ = playerZ,
                    HeadX = playerHeadX,
                    HeadY = playerHeadY,
                    HeadZ = playerHeadZ,
                    Health = playerHealth,
                    Team = playerTeam,
                    Dormant = playerDormant
                };

                playerList.Add(currentPlayer);
            }

            return playerList;
        }
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
