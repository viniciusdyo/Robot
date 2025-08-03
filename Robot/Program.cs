using Robot.Driver;
using System.Threading.Tasks;

namespace Robot;

internal class Program
{
    static async Task Main(string[] args)
    {
        const string BASE_URL = "https://localhost:7028/Endereco/";

        var request = new RequestProvider();
        var buscar = new BuscaCepDriver();

        while (true) { 
        
        var endereco = await request.GetAsync<EnderecoModel>(BASE_URL + "ObterCepParaTratamento?robo=robo'");

            buscar.BuscarCep(endereco);

            await request.PutAsync(BASE_URL + "AtualizarDados", endereco);

            Thread.Sleep(TimeSpan.FromSeconds(5));
        }
    }
}