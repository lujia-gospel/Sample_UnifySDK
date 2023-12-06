namespace com.ultrasdk.unity.Entry
{
    public class UltraGameRoleInfo
    {
        /// <summary>
        /// 渠道用户ID(玩家登录账号)
        /// </summary>
        public string channelUserId;
        
        /// <summary>
        /// 游戏生成的账号ID
        /// </summary>
        public string gameUserId;
        
        /// <summary>
        /// 区服ID
        /// </summary>
        public string serverId;
        
        /// <summary>
        /// 区服名称
        /// </summary>
        public string serverName;
        
        /// <summary>
        /// 角色ID
        /// </summary>
        public string roleId;
        
        /// <summary>
        /// 角色名
        /// </summary>
        public string roleName;
        
        /// <summary>
        /// 角色头像地址
        /// </summary>
        public string roleAvatar;

        /// <summary>
        /// 角色等级
        /// </summary>
        public string level ;
        
        /// <summary>
        /// 角色VIP等级
        /// </summary>
        public string vipLevel ;
        
        /// <summary>
        /// 一级货币[充值获得]
        /// </summary>
        public string gold1 ;
        
        /// <summary>
        /// 二级货币[游戏内产出]
        /// </summary>
        public string gold2 ;
        
        /// <summary>
        /// 累计充值总额
        /// </summary>
        public string sumPay ;
        
        public string levelExp ;
        public string vipScore ;
        public string rankLevel ;
        public string rankExp ;
        public string rankLeve2 ;
        public string rankExp2 ;
        public string cupCount1 ;
        public string cupCount2 ;
        public string totalKill ;
        public string totalHead ;
        public string avgKD ;
        public string maxKD ;
        public string maxCK ;
        public string mainWeaponId ;
        public string viceWeaponId ;
        public string medalCount ;
        public string teamId ;
        public string teamName ;
        public bool floatHidden ;

        //android
        public string partyName;
        public string roleCreateTime;
        public long balanceLevelOne;
        public long balanceLevelTwo;

        //android 360渠道独有
        public string partyId;
        public string roleGender;
        public string rolePower;
        public string partyRoleId;
        public string partyRoleName;
        public string professionId;
        public string profession;
        public string friendList;

    }
}