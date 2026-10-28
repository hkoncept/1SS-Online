using Newtonsoft.Json.Linq;
namespace NHLStats.Models;


public class Team
{
  public string Name { get; set; } = string.Empty;
  public string DivisionName { get; set; } = string.Empty;
  public string Abbreviation { get; set; } = string.Empty;
  public string Conference { get; set; } = string.Empty;


  /// <summary>
  /// Prend un token d'un objet Json et en crée une équipe en mémoire
  /// </summary>
  /// <param name="json">Token Json de l'équipe</param>
  /// <param name="language">fr ou default</param>
  /// <returns>Équipe créée</returns>
  public static Team FromJson(JToken json, string language = "fr")
  {
    return new Team
    {
      Name = json["teamName"]?[language]?.ToString() ?? json["teamName"]?["default"]?.ToString() ?? "",
      DivisionName = json["divisionName"]?.ToString() ?? "",
      Abbreviation = json["teamAbbrev"]?["default"]?.ToString() ?? "",
      Conference = json["conferenceName"]?.ToString() ?? "",
    };
  }
}
