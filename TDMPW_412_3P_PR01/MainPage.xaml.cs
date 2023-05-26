using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace TDMPW_412_3P_PR01;

public partial class MainPage : ContentPage
{

    String[] preguntas = new String[] {
    "¿De qué color es la caja negra de un avión?",
     "¿Cuál es la capital de Marruecos?",
     "¿Cuál es el animal nacional de Escocia?",
     "¿Cuál fue la primera princesa de Disney?",
     "Nombre del satélite natural de la Tierra",
     "Apellido de la persona que descubrió la penicilina",
     "¿Cuál es el planeta más caliente del sistema solar?",
     "Nombre del mineral más duro del planeta",
     "Nombre de las únicas sondas interestelares enviadas por la humanidad",
     "Nombre de la montaña más alta del mundo",
     "Año en que terminó la Segunda Guerra Mundial",
     "¿Cuál es la capital de Turquía?",
     "¿Cuál es el idioma oficial de Andorra?",
     "Nombre del río más largo del mundo",
     "Año de la caída de Constantinopla",
     "Nombre del dios egipcio del Sol",
     "Año en que se descubrió América",
     "Ciudad donde se encuentra la casa de Ana Frank",
     "Nombre del país más pequeño del mundo",
     "Año en que La Orden de los Caballeros Templarios se disolvió"
    };

    String[] respuestas = new String[]
    { "naranja", "rabat", "unicornio", "blancanieves", "luna", "fleming", "venus", 
        "diamante", "voyager", "everest", "1945", "ankara", "catalan", "nilo", 
        "1453", "ra", "1492", "amsterdam", "monaco", "1312" };

    int[] numbers = new int[20];

    int numeroPregunta = 0;
    int correctas = 0;
    int incorrectas = 0;
    int cantidadPreguntas = 0;
    int puntajeParaGanar = 3;
    int intentos = 2;
    bool juegoTerminado = false;

    public int Next(RNGCryptoServiceProvider random)
    {
        byte[] randomInt = new byte[4];
        random.GetBytes(randomInt);
        return Convert.ToInt32(randomInt[0]);
    }


    public MainPage()
	{
		InitializeComponent();
        inicializarJuego();
       
	}

    void inicializarJuego()
    {

        cargarNumerosDePreguntas();
        randomizarPreguntas();
        numeroPregunta = 0;
        correctas = 0;
        incorrectas = 0;
        cantidadPreguntas = 0;
        puntajeParaGanar = 3;
        intentos = 2;
        juegoTerminado = false;
        txtPregunta.Text = preguntas[numbers[numeroPregunta]];
        txtResultado.Text = "";
        txtRespuesta.Text = "";


    }

    void randomizarPreguntas()
    {

        RNGCryptoServiceProvider random = new RNGCryptoServiceProvider();
        numbers = numbers.OrderBy(x => Next(random)).ToArray();


    }

    void cargarNumerosDePreguntas()
    {

        for (int i = 0; i < preguntas.Length; i++) {
            numbers[i]=i;
        }

    }


    void mostrarSiguientePregunta()
    {

        numeroPregunta += 1;
        txtPregunta.Text = preguntas[numbers[numeroPregunta]];
        txtRespuesta.Text = "";
        intentos = 2;
       // imgPregunta.image = NSImage(named: "pregunta\(numbers[contador])")!;


    }


    void actualizarRespuestaCorrecta()
    {

        correctas++;
        txtPuntaje.Text = $"Puntos: {correctas}";
        txtResultado.Text = "Respuesta Correcta";


    }

    void actualizarRespuestaIncorrecta()
    {
        txtResultado.Text = "Respuesta Incorrecta";
        incorrectas++;

    }

    void checarPuntaje()
    {

        if (incorrectas == 2) {

            perderJuego();
  
      }

        else if (correctas == puntajeParaGanar) {

            ganarJuego();

            }
    }

    void terminarJuego()
    {
        juegoTerminado = true;
    }

    void perderJuego()
    {
        txtResultado.Text = "PERDISTE";
            terminarJuego();
    }

    void ganarJuego()
    {

        txtResultado.Text = "GANASTE";

            terminarJuego();

    }

    void validarRespuesta() {

        if (!juegoTerminado)
        {
            if (obtenerRespuesta() != respuestas[numbers[numeroPregunta]])
            {
                if (intentos < 2)
                {
                    actualizarRespuestaIncorrecta();
                    checarPuntaje();
                    if (!juegoTerminado)
                    {
                        mostrarSiguientePregunta();
                    }
                }
                else
                {
                    intentos--;
                    txtResultado.Text = "Respuesta Incorrecta, inténtalo de nuevo";
                }

            }

            else
            {
                actualizarRespuestaCorrecta();
                checarPuntaje();

                if (!juegoTerminado)
                {
                    mostrarSiguientePregunta();
                }
            }
        }
        }

    string obtenerRespuesta() {

        string respuestaOriginal = txtRespuesta.Text;
        string respuestaSinAcentos = Regex.Replace(respuestaOriginal.Normalize(NormalizationForm.FormD), @"[^a-zA-z0-9 ]+", "");
        string respuestaFormateada = respuestaSinAcentos.ToLower();

        return respuestaFormateada;
    }
    

    void btnComprobar_Clicked(System.Object sender, System.EventArgs e)
    {
        validarRespuesta();

    }

    void btnReiniciar_Clicked(System.Object sender, System.EventArgs e)
    {
        inicializarJuego();

    }


}

