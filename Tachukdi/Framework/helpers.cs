using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tachukdi.Framework
{
    public class helpers
    {
        private UserHelper _userhelper;
        public UserHelper UserHelper
        {
            get
            {
                if (_userhelper == null)
                    _userhelper = new UserHelper();
                return _userhelper;
            }
            set
            {
                _userhelper = value;
            }
        }

        private ConfigHelper _confighelper;
        public ConfigHelper ConfigHelper
        {
            get
            {
                if (_confighelper == null)
                    _confighelper = new ConfigHelper();
                return _confighelper;
            }
            set
            {
                _confighelper = value;
            }
        }

        private ReferralHelper _referralHelper;
        public ReferralHelper ReferralHelper
        {
            get
            {
                if (_referralHelper == null)
                    _referralHelper = new ReferralHelper();
                return _referralHelper;
            }
            set
            {
                _referralHelper = value;
            }
        }

        private BlockHelper _blockHelper;
        public BlockHelper BlockHelper
        {
            get
            {
                if (_blockHelper == null)
                    _blockHelper = new BlockHelper();
                return _blockHelper;
            }
            set
            {
                _blockHelper = value;
            }
        }

        private MoqDataHelper _dataHelper;
        public MoqDataHelper DataHelper
        {
            get
            {
                if (_dataHelper == null)
                    _dataHelper = new MoqDataHelper();
                return _dataHelper;
            }
            set
            {
                _dataHelper = value;
            }
        }

        private PointHelper _pointHelper;
        public PointHelper PointHelper
        {
            get
            {
                if (_pointHelper == null)
                    _pointHelper = new PointHelper();
                return _pointHelper;
            }
            set
            {
                _pointHelper = value;
            }
        }
    }
}
