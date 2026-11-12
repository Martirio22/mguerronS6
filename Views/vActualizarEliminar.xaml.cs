
using mguerronS6.Modelos;

namespace mguerronS6.Views;

public partial class vActualizarEliminar : ContentPage
{
    private const string Url = "http://192.168.17.22/ws_uisrael/ws_estudiante.php";
    private readonly HttpClient cliente = new HttpClient();

    public vActualizarEliminar(Estudiante datos)
    {
        InitializeComponent();

        txtCodigo.Text = datos.codigo.ToString();
        txtNombre.Text = datos.nombre;
        txtApellido.Text = datos.apellido;
        txtEdad.Text = datos.edad.ToString();
    }

    private async void btnActualizar_Clicked(object sender, EventArgs e)
    {
        try
        {
            bool confirmar = await DisplayAlert(
                "Confirmar",
                "¿Deseas actualizar este estudiante?",
                "Sí",
                "No");

            if (!confirmar)
                return;

            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtApellido.Text) ||
                !int.TryParse(txtEdad.Text, out int edad) ||
                edad < 0)
            {
                await DisplayAlert("Error", "Ingresa datos válidos.", "Aceptar");
                return;
            }

            string url = Url +
                "?codigo=" + Uri.EscapeDataString(txtCodigo.Text) +
                "&nombre=" + Uri.EscapeDataString(txtNombre.Text) +
                "&apellido=" + Uri.EscapeDataString(txtApellido.Text) +
                "&edad=" + edad;

            HttpResponseMessage respuesta = await cliente.PutAsync(url, null);

            if (respuesta.IsSuccessStatusCode)
            {
                await DisplayAlert("Éxito", "Estudiante actualizado correctamente.", "Aceptar");

                await Navigation.PushAsync(new vEstudiante());
            }
            else
            {
                await DisplayAlert("Error", "No se pudo actualizar el estudiante.", "Aceptar");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "Aceptar");
        }
    }

    private async void btnEliminar_Clicked(object sender, EventArgs e)
    {
        try
        {
            bool confirmar = await DisplayAlert(
                "Confirmar",
                "¿Estás seguro de eliminar este estudiante?",
                "Sí",
                "No");

            if (!confirmar)
                return;

            string url = Url +
                "?codigo=" + Uri.EscapeDataString(txtCodigo.Text);

            HttpResponseMessage respuesta = await cliente.DeleteAsync(url);

            if (respuesta.IsSuccessStatusCode)
            {
                await DisplayAlert("Éxito", "Estudiante eliminado correctamente.", "Aceptar");

                await Navigation.PushAsync(new vEstudiante());
            }
            else
            {
                await DisplayAlert("Error", "No se pudo eliminar el estudiante.", "Aceptar");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "Aceptar");
        }
    }
}
