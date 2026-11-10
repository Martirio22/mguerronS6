using mguerronS6.Modelos;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace mguerronS6.Views;

public partial class vEstudiante : ContentPage
{
	private const string Url = "http://192.168.17.22/ws_uisrael/ws_estudiante.php";
	private readonly HttpClient cliente = new HttpClient();
	private ObservableCollection<Estudiante> _estudiante;

    public vEstudiante()
	{
		InitializeComponent();
		Mostrar();

    }

	public async void Mostrar()
	{
		var content = await cliente.GetStringAsync(Url);
		List<Estudiante> mostrarEstudiante = JsonConvert.DeserializeObject<List<Estudiante>>(content);
		_estudiante = new ObservableCollection<Estudiante>(mostrarEstudiante);
		listaEstudiantes.ItemsSource = _estudiante;
	}


}