using System.Diagnostics;

namespace MatchGameMejorado4030347
{
    public partial class MainPage : ContentPage
    {
        Stopwatch stopwatch;
        int matchedPairs;

        public MainPage()
        {
            InitializeComponent();
            SetUpGame();
        }

        private void SetUpGame()
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();
            matchedPairs = 0;

            // Lista que contiene emojis duplicados para formarse los pares.
            List<string> animalEmoji = new List<string>()
            {
                "🐶","🐶",
                "🐒", "🐒",
                "🦑", "🦑",
                "🐘", "🐘",
                "🦒", "🦒",
                "🐍", "🐍",
                "🐬", "🐬"
            };

            Random random = new Random();
            foreach (var view in Grid1.Children)
            {
                if (view is Button button)
                {
                    if (animalEmoji.Count > 0)
                    {
                        // Selecciona un emoji aleatorio de la lista y lo asigna al botón.
                        int index = random.Next(animalEmoji.Count);
                        string nextEmoji = animalEmoji[index];
                        button.Text = nextEmoji;
                        button.IsVisible = true;  // Asegurarse de que el botón esté visible al inicio del juego
                        button.Clicked += Button_Clicked; // Asigna el mismo controlador de eventos para todos los botones.
                        animalEmoji.RemoveAt(index); // Elimina el emoji seleccionado de la lista para evitar duplicados.
                    }
                }
            }

            // Asegurarse de que el botón de reinicio esté oculto al iniciar el juego
            RestartButton.IsVisible = false;
        }

        // Manejador de eventos para el clic en los botones.
        Button ultimoButtonClicked;
        bool encontrandoMatch = false;

        private void Button_Clicked(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button == null || !button.IsVisible)
                return;

            if (encontrandoMatch == false)
            {
                // Oculta el botón y lo marca como el último botón clickeado.
                button.IsVisible = false;
                ultimoButtonClicked = button;
                encontrandoMatch = true;
            }
            else if (button.Text == ultimoButtonClicked.Text)
            {
                // Si se encuentra un emoji coincidente, oculta el botón actual y restablece el estado de búsqueda de coincidencia.
                button.IsVisible = false;
                encontrandoMatch = false;
                matchedPairs++;

                // Verifica si se han encontrado todos los pares.
                if (matchedPairs == 7)
                {
                    stopwatch.Stop();
                    DisplayAlert("¡Felicidades!", $"Has completado el juego en {stopwatch.Elapsed.TotalSeconds:F2} segundos.", "OK");

                    // Mostrar el botón de reinicio
                    RestartButton.IsVisible = true;
                }
            }
            else
            {
                // Si no hay coincidencia, muestra nuevamente el último botón clickeado y restablece el estado de búsqueda de coincidencia.
                ultimoButtonClicked.IsVisible = true;
                encontrandoMatch = false;
            }
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            // Método vacío, sin uso actualmente.
        }

        private void RestartButton_Clicked(object sender, EventArgs e)
        {
            // Reiniciar el juego
            SetUpGame();
        }
    }

}
