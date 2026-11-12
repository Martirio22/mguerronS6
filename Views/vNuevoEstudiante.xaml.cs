using System.Net;

namespace mguerronS6.Views;

public partial class vNuevoEstudiante : ContentPage
{
	public vNuevoEstudiante()
	{
		InitializeComponent();
	}

    private void btnAregar_Clicked(object sender, EventArgs e)
    {
		try
		{
			WebClient cliente = new WebClient();
			var parametros = new System.Collections.Specialized.NameValueCollection();
			parametros.Add("nombre", txtNombre.Text);
			parametros.Add("apellido", txtApellido.Text);
			parametros.Add("edad", txtEdad.Text);

			cliente.UploadValues("http://localhost/ws_uisrael/ws_estudiante.php", "POST", parametros);
			Navigation.PushAsync(new vEstudiante());
		}
		catch (Exception)
		{

			throw;
		}
    }
}