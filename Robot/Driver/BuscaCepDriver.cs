using EasyAutomationFramework;
using OpenQA.Selenium;
using System.Text.RegularExpressions;

namespace Robot.Driver;

public class BuscaCepDriver : Web
{
    public BuscaCepDriver()
    {
        StartBrowser();
    }

    public void BuscarCep(EnderecoModel endereco)
    {
        Navigate("https://buscacep.com.br/");

        AssignValue(TypeElement.Name, "cep", endereco.CEP, 2)
            .element.SendKeys(Keys.Enter);
        ;
        endereco.Logradouro = GetValue(TypeElement.Xpath, "/html/body/main/div/div/div[3]/div[2]/div[2]/div[1]/div/p").Value.TrimStart();
        endereco.Bairro = GetValue(TypeElement.Xpath, "/html/body/main/div/div/div[3]/div[2]/div[2]/div[3]/div/p").Value.TrimStart();
        endereco.UF = ExtrairUF(GetValue(TypeElement.Xpath, "/html/body/main/div/div/div[3]/div[2]/div[2]/div[5]/div/p").Value.TrimStart());

        Console.WriteLine(endereco.Logradouro);
        Console.WriteLine(endereco.Bairro);
        Console.WriteLine(endereco.UF);
    }

    private string ExtrairUF(string texto)
    {
        Match match = Regex.Match(texto, @"\(([A-Z]{2})\)");

        if (match.Success)
        {
            var uf = match.Groups[1].Value;
            VerificarUF(uf);
            return uf;
        }
        return string.Empty;
    }

    private void VerificarUF(string uf)
    {
        if (string.IsNullOrWhiteSpace(uf))
            throw new Exception("UF inválido.");
    }
}
