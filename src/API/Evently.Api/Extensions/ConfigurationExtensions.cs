namespace Evently.Api.Extensions;

public static class ConfigurationExtensions
{
    internal static void AddModuleConfigration(
        this IConfigurationBuilder configurationBuilder,
        string[] modules)
    {
        foreach (var item in modules)
        {
            configurationBuilder.AddJsonFile
                ($"modules.{item}.json",
                false,
                true);
          
            
            configurationBuilder.AddJsonFile($"modules.{item}.Development.json", true, true);
        }
    }
}
