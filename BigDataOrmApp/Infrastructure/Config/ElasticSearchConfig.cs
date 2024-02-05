namespace BigDataOrmApp.Infrastructure.Config;

public record ElasticSearchConfig
{
    public string Url { get; set; }
    public string DefaultIndex { get; set; }
    public string CertFingerprint { get; set; }
    public string UserPassword { get; set; }
    public string UserName { get; set; }
}