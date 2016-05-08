namespace WebUI.AppConfiguration
{
    public interface IAppConfiguration
    {
        string AccessKey { get; }
        string SecretKey { get;  }
        string ServiceUrl { get;  }
    }
}
