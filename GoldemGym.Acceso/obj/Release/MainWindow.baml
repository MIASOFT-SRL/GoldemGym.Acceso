   M S B A M L   `   `   `  ����   GoldemGym.Acceso!    GoldemGym.Acceso.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     �Name$  frmPrincipal�� � Title$ MIASOFT Control de Acceso��$	��623��$	��844�� � WindowStartupLocation$ CenterScreen=� � ToolTip$ MIASOFT Control de Acceso��$y�Visible=�$��0q� � WindowState$ 	Maximized=� � WindowStyle$
 None=� � 	Resources  ��Name   AnimacionOpaca g� Duration	 W� From	
 W� To �� TargetProperty �� 
TargetName��%q   &	        �� $ AnimacionOpaca��.��W� $ 00:00:01.50D�$	 0X�$
 10X�$ SkewTransform�$ imgLogo������  �� ImageSource$# img/logo golden gim copia.jpg��.���� -    ��Name$ BorderPrincipal��$��4q�$��	10,10,0,0q� �� CornerRadius$ 20��$��Center=�$	��801��$	��524��$��Center=����	#FF0A229E J� Opacity$
 0.95Q�._�� $��-4,3,4,6q� J� OpacityMask
 �   � � ColumnDefinitions �� $
��792* ���  � RowDefinitions �� .+�� $
��Left=�$	��770��$��	0,0,0,357q� ��  �� .+��� $	��160��$��10,174,0,-184q�$	��Top=�$
��Left=�$	��430��.+��� $��
Codigo :
��$��18(�$��92�����	#FFF91429$
��Bold"�$��33��$��169,0q�$��Center=�$��Center=��� ���	#FF0F0F0F$��2q�$ 10��._��� -    ��Name$ 	lblCodigo��$��No definido��$��Center=�$��Center=�$��18(����	#FFFE8003$
��Bold"��� $��	Nombre :
��$��18(����	#FFF91429$
��Bold"�$��43��$��Center=�$��Center=��� ���	#FF0F0F0F$��2q�$ 10��$��43��$��Center=�$��Center=�$	��430��._��� -   $ 	lblNombre��$��No definido��$��Center=�$��Center=�$��18(����	#FFFE8003$
��Bold"��� $��445,23,0,-184q�.+��� $��Días Faltantes��$��18(�$��Center=����	#FFF91429$
��Bold"�$��	47,0,63,0q�$��Center=��� ���	#FF0F0F0F$��3q�$ 10��$	��275��$��	0,0,-12,0q�._��� -   $ lblDiasFaltantes��$��0�����	#FFFEFEFE$
��Bold"�$��7,-3,7,7q�$	��175(�$��Center=��� $��	0,343,9,0q�$	��154��$	��Top=�$��Right=�$	��773��.+��� $��Fecha Inicio��$��18(�$��Center=�$��Center=����	#FFF91429$
��Bold"��� ���	#FF0F0F0F$	��2.5q�$��40��$ 10��._��� -   $ lblFechaInicio��$��No definido��$��Center=�$��Center=�$��18(����	#FFFE8003$
��Bold"��� $��Fecha de Expiración��$��Center=�$��Center=�$��18(����	#FFF91429$
��Bold"��� ���	#FF0F0F0F$	��2.5q�$��40��$ 10��._��� -   $ lblFechaFin��$��No definido��$��Center=�$��Center=�$��18(����	#FFFE8003$
��Bold"��� $
��Left=�$��35��$��0,527,-3,-55q�$	��Top=�$	��796�� �� Orientation$ 
Horizontal=�$��2��.+��� $&�� COPY RIGHT 2014 "MIASOFT s.r.l."�� J� RenderTransformOrigin$ -0.309,0.519&�$
��Bold"����	#FF0029FF�� $#��Correo : miasoft.bo@gmail.com��$
��Bold"����	#FF0029FF�� $/��)Teléfono: 72634488 - 71010967 - 60895096��
������ $
��Bold"����	#FF0029FF�� $��0,0,0,10q�$	��145�����	#FFF5A525$ 10��._��� -    ��Name$ btnSalir��$��Salir��$	��Top=�$��25��$��0q����	#00000000$
��Left=�$	��145���� $
��Left=�$	��115��$��124,-22,0,0q�$	��Top=�$	��154��.+��� -	    ��Name$ imgLogo��$	��114��$	��214��$#��img/logo golden gim copia.jpg��$
��Fill=�$ 0.5,0.5&�
 �� Top$ 66�� �� Left$ 2��z�V� .~��� �� -
   
Traslacion�� �� AngleX$ 10Q� �� AngleY$ 10Q��� -   Rotacion�� �� Angle$ 10Q�T�  ��  �� EndPoint$ 0.5,1&� �� 
StartPoint$ 0.5,0&�.���   � Color$  Black��! � Offset$! 0Q�� $  White��$! 1Q�