using Robot.Driver;

namespace Robot;

internal class Program
{
    static void Main(string[] args)
    {
        var buscar = new BuscaCepDriver();

        buscar.BuscarCep(new EnderecoModel
        {
            CEP = "11015300"
        });
    }
}