using EasyAutomationFramework;
using Sikuli4Net.sikuli_REST;

namespace Robot.Driver;

public class BuscaCepDriver : Web
{
    public BuscaCepDriver()
    {
        StartBrowser();
    }

    public void BuscarCep(EnderecoModel endereco)
    {
        Navigate("https://www.cepdobrasil.com.br/");
        AssignValue(TypeElement.Id, "cep", endereco.CEP).element.SendKeys(Key.ENTER);
        endereco.Logradouro = GetValue(TypeElement.CssSelector, "body > section > div > article > div:nth-child(4) > div.resultado > div.endereco > div > div.blocoendereco > a:nth-child(1) > span:nth-child(2)").element.Text;
        endereco.Bairro = ExtrairTextoAposDoisPontos(GetValue(TypeElement.CssSelector, "body > section > div > article > div:nth-child(4) > div.resultado > div.endereco > div > div.blocoendereco > a:nth-child(4) > span").element.Text);
        endereco.UF = GetValue(TypeElement.CssSelector, "body > section > div > article > div:nth-child(4) > div.resultado > div.endereco > div > div.blocoendereco > span:nth-child(10)").element.Text;
    }

    private string ExtrairTextoAposDoisPontos(string texto)
    {
        return texto.Split(':')[1].Trim();
    }
}
