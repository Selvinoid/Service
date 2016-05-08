using System.Configuration;

namespace WebUI.AppConfiguration
{
    public class AppConfiguration : IAppConfiguration
    {
        public string AccessKey
        {
            get
            {
                var appSetting = ConfigurationManager.AppSettings["AccessKey"];

                if (string.IsNullOrEmpty(appSetting))
                {
                    throw new ConfigurationErrorsException("AccessKey parameter is not specified in app.config file.");
                }

                return appSetting;
            }
        }

        public string SecretKey
        {
            get
            {
                var secretKey = ConfigurationManager.AppSettings["SecretKey"];

                if (string.IsNullOrEmpty(secretKey))
                {
                    throw new ConfigurationErrorsException("SecretKey parameter is not specified in app.config file.");
                }

                return secretKey;
            }
        }

        public string ServiceUrl
        {
            get
            {
                var image = ConfigurationManager.AppSettings["AmazonServiceUrl"];

                if (string.IsNullOrEmpty(image))
                {
                    throw new ConfigurationErrorsException("AmazonServiceUrl parameter is not specified in app.config file.");
                }

                return image;
            }
        }

       

        public AppConfiguration()
        {
        }

        private static AppConfiguration _appConfiguration;


        public static AppConfiguration Instance
        {
            get
            {
                if (_appConfiguration == null)
                {
                    lock (typeof(AppConfiguration))
                    {
                        if (_appConfiguration == null)
                            _appConfiguration = new AppConfiguration();
                    }
                }

                return _appConfiguration;
            }
        }
    }
}
