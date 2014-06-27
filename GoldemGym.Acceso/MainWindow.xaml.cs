using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Data;

namespace GoldemGym.Acceso
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region "Atributos"
        public zkemkeeper.CZKEM dispositivo;
        private int iMachineNumber;
        #endregion

        #region "Propiedades"
        private int Port { set; get; }
        private string IP { set; get; }
        private string RutaDB { set; get; }
        private string BD { set; get; }
        private string Servidor { set; get; }
        private string Usuario { set; get; }
        private string Contraseña { set; get; }
        private bool ModoMixto { set; get; }
        private bool IsConected { get; set; }
        private bool IsConfiguration { set; get; }
        private System.Windows.Threading.DispatcherTimer Timer { set; get; }
        private System.Windows.Threading.DispatcherTimer TimerConexion { set; get; }
        private int SecondsTimer { set; get; }
        private int SecondsTimerConexion { set; get; }
        private bool IsTimerConexion { set; get; }
        /// <summary>
        /// Se obtiene o establece la forma de contectarse a la base de datos (Cadena de Conexión)
        /// </summary>
        private int Opcion { set; get; }

        #endregion

        #region "Metodos"

        private void limpiar()
        {
            string text = "No definido";
            lblCodigo.Content = text;
            lblNombre.Content = text;
            lblFechaInicio.Content = text;
            lblFechaFin.Content = text;
            lblDiasFaltantes.Content = 0;
            lblDiasFaltantes.Foreground = Brushes.White;
            animacion_imgLogoReset();
        }

        private void timerBegin()
        {
            Timer = new System.Windows.Threading.DispatcherTimer();
            Timer.Tick += new EventHandler(Timer_Tick);
            Timer.Interval = new TimeSpan(0, 0, SecondsTimer);
        }

        private void timerBeginConexion()
        {
            TimerConexion = new System.Windows.Threading.DispatcherTimer();
            TimerConexion.Tick += new EventHandler(TimerConexion_Tick);
            TimerConexion.Interval = new TimeSpan(0, 0, SecondsTimerConexion);
            timerStarConexion();
        }

        private void timerStar()
        {
            Timer.Start();
        }

        private void timerStarConexion()
        {
            TimerConexion.Start();
        }

        private void timerStop()
        {
            Timer.Stop();
        }

        private void timerStopConexion()
        {
            TimerConexion.Stop();
        }

        private void Desconectar()
        {
            Cursor = Cursors.Wait;
            if (IsConected)
            {
                dispositivo.Disconnect();
                this.dispositivo.OnAttTransactionEx -= new zkemkeeper._IZKEMEvents_OnAttTransactionExEventHandler(Dispositivo_OnAttTransactionEx);
                IsConected = false;
                Cursor = Cursors.Arrow;
            }
        }

        private bool Conectar()
        {
            int idwErrorCode = 0;
            Cursor = Cursors.Wait;
            IsConected = dispositivo.Connect_Net(IP, Port);
            if (IsConected)
            {
                iMachineNumber = 1;

                if (dispositivo.RegEvent(iMachineNumber, 65535))//Here you can register the realtime events that you want to be triggered(the parameters 65535 means registering all)
                {
                    this.dispositivo.OnAttTransactionEx += new zkemkeeper._IZKEMEvents_OnAttTransactionExEventHandler(Dispositivo_OnAttTransactionEx);
                    Cursor = Cursors.Arrow;
                    return true;
                }
            }
            else
            {
                dispositivo.GetLastError(ref idwErrorCode);
            }
            Cursor = Cursors.Arrow;
            return false;
        }

        private void setConfiguration()
        {
            //Obteniendo las configuraciones para la aplicacion
            IsConfiguration = true;
            string directorio = Directory.GetCurrentDirectory();
            string configuracion = directorio + "\\Configuraciones.txt";
            if (!File.Exists(configuracion))
            {
                MessageBox.Show("No existe el archivo de configuraciones.txt", "Configuraciones", MessageBoxButton.OK, MessageBoxImage.Error);
                IsConfiguration = false;
                return;
            }

            using (StreamReader archivo = new StreamReader(configuracion))
            {
                String datos = archivo.ReadLine();
                if (datos != null)
                {
                    string[] lista = datos.Split(';');
                    if (lista.Length != 11)
                    {
                        MessageBox.Show("El archivo de configuraciones debe contener 11 campos, separados por punto y coma (;)", "Configuraciones", MessageBoxButton.OK, MessageBoxImage.Error);
                        IsConfiguration = false;
                        return;
                    }
                    RutaDB = lista[0];//Ruta de la base de datos Acces
                    IP = lista[1];//IP del dispositivo con el que se va a conectar
                    Servidor = lista[2];//Nombre o IP del servidor de datos del control de acceso
                    BD = lista[3];//Base de datos del control de acceso, si esta fuera SQL SERVER
                    Usuario = lista[4];//Usuario para conectarse a la base de datos
                    Contraseña = lista[5];//Contraseña para conectarse a la base de datos
                    if (lista[6].Equals("0"))//Modo de autenticación a la bd, si es mixto (1) o si es autenticación windows (0)
                    {
                        ModoMixto = false;
                    }
                    else
                    {
                        if (lista[6].Equals("1"))
                        {
                            ModoMixto = true;
                        }
                        else
                        {
                            MessageBox.Show("El campo modo mixto no es un valor booleano", "Configuraciones", MessageBoxButton.OK, MessageBoxImage.Error);
                            IsConfiguration = false;
                            return;
                        }
                    }

                    string op = lista[7];
                    if (op.Equals(string.Empty))//Determina que opción se require para la conexión a la base de datos; 1 Ado.Net Entity Framework, 2 Ado.net y 3 Microsoft Acces
                    {
                        MessageBox.Show("Esta vació el campo que determina la opcion de conexión a la bae de datos en el archivo de configuraciones", "Configuraciones", MessageBoxButton.OK, MessageBoxImage.Error);
                        IsConfiguration = false;
                        return;
                    }
                    int opc = 0;
                    if (int.TryParse(op, out opc))
                    {
                        Opcion = opc;
                    }
                    else
                    {
                        MessageBox.Show("El campo opción tiene que ser un número entero", "Configuraciones", MessageBoxButton.OK, MessageBoxImage.Error);
                        IsConfiguration = false;
                        return;
                    }

                    string seg = lista[8];
                    if (seg.Equals(string.Empty))//Determina los segundos en Timer para realizar el refresh de la aplicación
                    {
                        MessageBox.Show("Esta vació el campo que determina los segundos en Timer", "Configuraciones", MessageBoxButton.OK, MessageBoxImage.Error);
                        IsConfiguration = false;
                        return;
                    }
                    int s = 0;
                    if (int.TryParse(seg, out s))
                    {
                        SecondsTimer = s;
                    }
                    else
                    {
                        MessageBox.Show("El campo opción tiene que ser un número entero", "Configuraciones", MessageBoxButton.OK, MessageBoxImage.Error);
                        IsConfiguration = false;
                        return;
                    }

                    seg = lista[9];
                    if (seg.Equals(string.Empty))//Determina los segundos en Timer para realizar el refresh de la aplicación
                    {
                        MessageBox.Show("Esta vació el campo que determina los segundos en Timer", "Configuraciones", MessageBoxButton.OK, MessageBoxImage.Error);
                        IsConfiguration = false;
                        return;
                    }
                    s = 0;
                    if (int.TryParse(seg, out s))
                    {
                        SecondsTimerConexion = s;
                    }
                    else
                    {
                        MessageBox.Show("El campo opción tiene que ser un número entero", "Configuraciones", MessageBoxButton.OK, MessageBoxImage.Error);
                        IsConfiguration = false;
                        return;
                    }

                    if (lista[10].Equals("0"))//Determina si se va a desconectar y contectar cada vez que se realice el refresh de la aplicación (Valor booleano, 0 y 1)
                    {
                        IsTimerConexion = false;
                    }
                    else
                    {
                        if (lista[10].Equals("1"))
                        {
                            IsTimerConexion = true;
                        }
                        else
                        {
                            MessageBox.Show("El campo IsTimerConexion no es un valor booleano", "Configuraciones", MessageBoxButton.OK, MessageBoxImage.Error);
                            IsConfiguration = false;
                            return;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("no existen datos");
                }
            }
        }

        private void calcularDiferenciaDia(DateTime fechaInicio, DateTime fechaFin)
        {
            DateTime fechaHoy = DateTime.Now;
            TimeSpan ts = fechaFin - fechaHoy;
            int difereciaDias = 0;
            if (ts.Days == 0 && ts.Hours > 0)
            {
                difereciaDias = 1;
            }
            else
            {
                difereciaDias = ts.Days;
            }

            lblDiasFaltantes.Content = difereciaDias.ToString();
            lblFechaFin.Content = fechaFin.ToLongDateString();
            lblFechaInicio.Content = fechaInicio.ToLongDateString();

            if (difereciaDias <= 5)
            {
                if (difereciaDias >= 0)
                {
                    lblDiasFaltantes.Foreground = Brushes.Red;
                }
                else
                {
                    lblDiasFaltantes.Foreground = Brushes.Red;
                }
            }
            else
            {
                lblDiasFaltantes.Foreground = Brushes.White;
            }
        }

        /// <summary>
        /// Procesa datos con una conexión a datos ADO.NET Entity Framework,
        /// que no es compatible con la version de motor de base de datos SQL SERVER 2000
        /// </summary>
        /// <param name="sEnrollNumber"></param>
        private void procesarSQLDataEntity(string sEnrollNumber)
        {
            //Codigo del evento que se ejecutara cuando se active una hella en el dispositivo
            Datos.dbControlAccesoEntities db = new Datos.dbControlAccesoEntities();
            lblCodigo.Content = sEnrollNumber;
            Datos.USERINFO ui = db.USERINFO.FirstOrDefault(u => u.BADGENUMBER == sEnrollNumber);
            

            if (ui != null)
            {
                lblNombre.Content = ui.NAME;
                DateTime fechaFin = Convert.ToDateTime(ui.HIREDDAY);
                DateTime fechaInicio = Convert.ToDateTime(ui.BIRTHDAY);
                calcularDiferenciaDia(fechaInicio, fechaFin);
                animacion_imgLogo();

            }
            else
            {
                //MessageBox.Show("No existe en la base de datos", "Control de Acceso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                lblNombre.Content = "No existe en la base de datos";
            }
        }
        /// <summary>
        /// Procesa datos con una conexión a datos de cualquier versión de motor de base de datos
        /// </summary>
        /// <param name="sEnrollNumber"></param>
        private void procesarSQL_ADO(string sEnrollNumber)
        {
            Datos.DAL.TDatosSql db = new Datos.DAL.TDatosSql(Servidor, BD, string.Empty, Usuario, Contraseña, ModoMixto);
            lblCodigo.Content = sEnrollNumber;
            object[] arg = new object[1];
            arg[0] = sEnrollNumber;
            DataTable ui = db.TraerDataTable("sp_getUserInfo", arg);

            if (ui != null && ui.Rows.Count > 0)
            {
                lblNombre.Content = ui.Rows[0]["NAME"].ToString();
                DateTime fechaFin = new DateTime();
                DateTime fechaInicio = new DateTime();
                string fecIni = ui.Rows[0]["BIRTHDAY"].ToString();
                string fecFin = ui.Rows[0]["HIREDDAY"].ToString();
                if (!string.IsNullOrEmpty(fecIni))
                {
                    if (!string.IsNullOrEmpty(fecFin))
                    {
                        fechaInicio = Convert.ToDateTime(fecIni);
                        fechaFin = Convert.ToDateTime(fecFin);
                        calcularDiferenciaDia(fechaInicio, fechaFin);
                    }
                    else
                    {
                        lblFechaFin.Content = "No tiene asiganda la fecha de finalización.";
                    }
                }
                else
                {
                    lblFechaInicio.Content = "No tiene asiganda la fecha de inicio.";
                }
                animacion_imgLogo();
            }
            else
            {
                //MessageBox.Show("No existe en la base de datos", "Control de Acceso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                lblNombre.Content = "No existe en la base de datos";
            }
        }
        /// <summary>
        /// Procesa datos con una conexión a datos Microsoft Acces.
        /// </summary>
        /// <param name="sEnrollNumber"></param>
        private void procesarMicrosoftAcces(string sEnrollNumber)
        {

            if (!File.Exists(RutaDB))
            {
                //MessageBox.Show("No existe el archivo de la base de datos Microsoft Acces", "Base de datos", MessageBoxButton.OK, MessageBoxImage.Error);
                lblNombre.Content = "No existe el archivo de la base de datos Microsoft Acces";
                return;
            }
            Datos.DAL.aDatosAcces db = new Datos.DAL.aDatosAcces(RutaDB);
            lblCodigo.Content = sEnrollNumber;

            DataTable ui = db.TraerDataTable("select * from USERINFO where BADGENUMBER = '" + sEnrollNumber + "'");
            if (ui != null && ui.Rows.Count > 0)
            {
                lblNombre.Content = ui.Rows[0]["NAME"].ToString();
                DateTime fechaFin = new DateTime();
                DateTime fechaInicio = new DateTime();
                string fecIni = ui.Rows[0]["BIRTHDAY"].ToString();
                string fecFin = ui.Rows[0]["HIREDDAY"].ToString();
                if (!string.IsNullOrEmpty(fecIni))
                {
                    if (!string.IsNullOrEmpty(fecFin))
                    {
                        fechaInicio = Convert.ToDateTime(fecIni);
                        fechaFin = Convert.ToDateTime(fecFin);
                        calcularDiferenciaDia(fechaInicio, fechaFin);
                    }
                    else
                    {
                        lblFechaFin.Content = "No tiene asiganda la fecha de finalización.";
                    }
                }
                else
                {
                    lblFechaInicio.Content = "No tiene asiganda la fecha de inicio.";
                }
                animacion_imgLogo();
            }
            else
            {
                //MessageBox.Show("No existe en la base de datos", "Control de Acceso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                lblNombre.Content = "No existe en la base de datos";
            }
        }
        #endregion

        #region "Animaciones"

        private void animacion_imgLogo()
        {
            //System.Windows.Media.Animation.Storyboard AnimacionOpaca = (System.Windows.Media.Animation.Storyboard)FindResource("AnimacionOpaca");
            //AnimacionOpaca.Begin();
            System.Windows.Media.Animation.DoubleAnimation animacionX = new System.Windows.Media.Animation.DoubleAnimation();

            animacionX.From = 10;
            animacionX.To = 0;
            animacionX.Duration = new Duration(TimeSpan.FromMilliseconds(1000));

            Traslacion.BeginAnimation(SkewTransform.AngleXProperty, animacionX);

            System.Windows.Media.Animation.DoubleAnimation animacionY = new System.Windows.Media.Animation.DoubleAnimation();

            animacionY.From = 10;
            animacionY.To = 0;
            animacionY.Duration = new Duration(TimeSpan.FromMilliseconds(1000));

            Traslacion.BeginAnimation(SkewTransform.AngleYProperty, animacionY);

            System.Windows.Media.Animation.DoubleAnimation animacionRotacion = new System.Windows.Media.Animation.DoubleAnimation();

            animacionRotacion.From = 10;
            animacionRotacion.To = 0;
            animacionRotacion.Duration = new Duration(TimeSpan.FromMilliseconds(1000));

            Rotacion.BeginAnimation(RotateTransform.AngleProperty, animacionRotacion);
        }

        private void animacion_imgLogoReset()
        {
            System.Windows.Media.Animation.DoubleAnimation animacionX = new System.Windows.Media.Animation.DoubleAnimation();

            animacionX.From = 0;
            animacionX.To = 10;
            animacionX.Duration = new Duration(TimeSpan.FromMilliseconds(1000));

            Traslacion.BeginAnimation(SkewTransform.AngleXProperty, animacionX);

            System.Windows.Media.Animation.DoubleAnimation animacionY = new System.Windows.Media.Animation.DoubleAnimation();

            animacionY.From = 0;
            animacionY.To = 10;
            animacionY.Duration = new Duration(TimeSpan.FromMilliseconds(1000));

            Traslacion.BeginAnimation(SkewTransform.AngleYProperty, animacionY);

            System.Windows.Media.Animation.DoubleAnimation animacionRotacion = new System.Windows.Media.Animation.DoubleAnimation();

            animacionRotacion.From = 0;
            animacionRotacion.To = 10;
            animacionRotacion.Duration = new Duration(TimeSpan.FromMilliseconds(1000));

            Rotacion.BeginAnimation(RotateTransform.AngleProperty, animacionRotacion);
        }

        #endregion

        #region"Events"

        private void Dispositivo_OnAttTransactionEx(string sEnrollNumber, int iIsInValid, int iAttState, int iVerifyMethod, int iYear, int iMonth, int iDay, int iHour, int iMinute, int iSecond, int iWorkCode)
        {
            try
            {
                switch (Opcion)
                {
                    case 1:
                        procesarSQLDataEntity(sEnrollNumber);
                        break;
                    case 2:
                        procesarSQL_ADO(sEnrollNumber);
                        break;
                    case 3:
                        procesarMicrosoftAcces(sEnrollNumber);
                        break;
                    default:
                        MessageBox.Show("La opción que introdujo en el archivo de configuraciones no es la correcta", "Configuraciones", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;
                }
                if (Timer.IsEnabled)
                {
                    timerStop();
                    timerStar();
                }
                else
                {
                    timerStar();
                }
            }
            catch (Exception r)
            {
                lblFechaInicio.Content = "Error interno " + r.Message;
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timerStop();
            limpiar();
        }

        private void TimerConexion_Tick(object sender, EventArgs e)
        {
            timerStopConexion();
            Desconectar();
            Conectar();
            timerStarConexion();
        }

        #endregion

        public MainWindow()
        {
            InitializeComponent();
            dispositivo = new zkemkeeper.CZKEM();
            IsConected = false;
            Port = 4370;
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Desea cerrar el programa?", "Conexión", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }

        }

        private void frmPrincipal_Loaded(object sender, RoutedEventArgs e)
        {
            setConfiguration();

            if (!IsConfiguration)
            {
                return;
            }



            if (Conectar())
            {
                timerBegin();
                if (IsTimerConexion)
                {
                    timerBeginConexion();
                }
                //MessageBox.Show("Se conecto correctamente dispositivo", "Conexión", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            else
            {
                MessageBox.Show("No se conecto correctamente dispositivo", "Conexión", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void frmPrincipal_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsConfiguration)
            {
                if (IsConected)
                {
                    timerStop();
                    if (IsTimerConexion)
                    {
                        timerStopConexion();
                    }
                    Desconectar();
                }
            }
        }
    }
}
