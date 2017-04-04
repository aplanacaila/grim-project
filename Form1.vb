Imports System.IO.Ports

Public Class Form1

    'variables
    Dim form_2 As Form2
    Dim rebre, enviar As String
    Dim Terreno(9, 9) As Integer    'Es la matriz que guarda el estado del terreno , es de 10x10
    Dim agvx As Integer             'Posición del AGV en el eje X e Y
    Dim agvy As Integer

    'Carga del formulario
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Inicializa_terreno()        'Inicializa el estado del terreno con todo ceros.

    End Sub


    'Esta Función verifica la proximidad de obstáculos, bloquea los botones de dirección para impedir el movimiento 
    Private Sub Comprueba_sensores()

        'sensor izquierda
        If agvx = 0 Then
            ButtonI.FlatStyle = FlatStyle.Standard
            ButtonI.BackColor = Color.Red
            enviar = "I"
        ElseIf Terreno(agvx - 1, agvy) = 0 Then
            ButtonI.FlatStyle = FlatStyle.Standard
            ButtonI.BackColor = Color.Red
            enviar = "I"
        Else
            ButtonI.FlatStyle = FlatStyle.System
            ButtonI.BackColor = SystemColors.Control
            enviar = "i"

        End If
        'sensor derecha
        If agvx = 9 Then
            Button6.FlatStyle = FlatStyle.Standard
            Button6.BackColor = Color.Red
            enviar = "D"
        ElseIf Terreno(agvx + 1, agvy) = 0 Then
            Button6.FlatStyle = FlatStyle.Standard
            Button6.BackColor = Color.Red
            enviar = "D"
        Else
            Button6.FlatStyle = FlatStyle.System
            Button6.BackColor = SystemColors.Control
            enviar = "d"
        End If
        'sensor arriba
        If agvy = 0 Then
            Button4.FlatStyle = FlatStyle.Standard
            Button4.BackColor = Color.Red
            enviar = "P"
        ElseIf Terreno(agvx, agvy - 1) = 0 Then
            Button4.FlatStyle = FlatStyle.Standard
            Button4.BackColor = Color.Red
            enviar = "P"
        Else
            Button4.FlatStyle = FlatStyle.System
            Button4.BackColor = SystemColors.Control
            enviar = "p"
        End If
        'sensor abajo
        If agvy = 9 Then
            Button7.FlatStyle = FlatStyle.Standard
            Button7.BackColor = Color.Red
            enviar = "A"
        ElseIf Terreno(agvx, agvy + 1) = 0 Then
            Button7.FlatStyle = FlatStyle.Standard
            Button7.BackColor = Color.Red
            enviar = "A"
        Else
            Button7.FlatStyle = FlatStyle.System
            Button7.BackColor = SystemColors.Control
            enviar = "a"
        End If

    End Sub


    'Este bucle recorre la matriz de 10x10 y la llena de 0
    Private Sub Inicializa_terreno()

        For idy = 0 To 9
            For idx = 0 To 9
                Terreno(idx, idy) = 0
            Next idx
        Next idy
    End Sub


    'Esta función lee la matriz de terreno y dibuja su represtentación en el PictureBox
    Private Sub Redibuja_terreno()

        'Inicializa el area de trabajo
        Dim p1, p2, p3, p4 As Point
        Dim Grid As Graphics
        Grid = PictureBox1.CreateGraphics
        Grid.Clear(Color.Gray)

        'Dibuja la rejilla de 10x10
        For idx = 1 To 9
            Grid.DrawLine(Pens.PowderBlue, idx * 40, 0, idx * 40, 400)
            Grid.DrawLine(Pens.PowderBlue, 0, idx * 40, 400, idx * 40)
        Next idx

        'Escanea la matriz del terreno y pone una caja roja en cada casilla marcada como 0 y una caja blanca en la que está marcada con un 2
        For idy = 0 To 9
            For idx = 0 To 9
                If Terreno(idx, idy) = 0 Then
                    p1.X = (idx * 40) + 5
                    p1.Y = (idy * 40) + 5
                    p2.X = (idx * 40) + 35
                    p2.Y = (idy * 40) + 5
                    p3.X = (idx * 40) + 35
                    p3.Y = (idy * 40) + 35
                    p4.X = (idx * 40) + 5
                    p4.Y = (idy * 40) + 35
                    Dim caja As Point() = {p1, p2, p3, p4}
                    Grid.FillPolygon(Brushes.DarkRed, caja)
                ElseIf Terreno(idx, idy) = 2 Then
                    p1.X = (idx * 40) + 5
                    p1.Y = (idy * 40) + 5
                    p2.X = (idx * 40) + 35
                    p2.Y = (idy * 40) + 5
                    p3.X = (idx * 40) + 35
                    p3.Y = (idy * 40) + 35
                    p4.X = (idx * 40) + 5
                    p4.Y = (idy * 40) + 35
                    Dim caja As Point() = {p1, p2, p3, p4}
                    Grid.FillPolygon(Brushes.Azure, caja)


                End If
            Next idx
        Next idy

    End Sub

    'Botón que genera el area de trabajo
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        'Esta función se activa al pulsar 'GENERAR NUEVO TERRENO'
        Dim punterox, punteroy As Integer

        'Inicializa la matriz de terreno
        Inicializa_terreno()

        'Utiliza un algorismo de Excavación para perforar n espacios

        'Escoge el punto de inicio aleatoriamente
        Randomize()
        punterox = Int((Rnd() * 10))
        punteroy = Int((Rnd() * 10))

        Terreno(punterox, punteroy) = 1 'Le asigan el valor 1 (punto hueco)

        For n = 0 To 150 'Número de excavaciones

            'En este select sale la dirección de excavación aleatoriamente en cada iteración (ARRIBA, ABAJO, IZQUIERA O DERECHA)
            Select Case Int((Rnd() * 4))
                Case 0 'arriba'
                    If punteroy > 0 Then 'Solo subo si no estoy arriba del todo, si no puedo subir se descuenta la iteración
                        punteroy = punteroy - 1
                    Else
                        n = n - 1
                    End If
                Case 1 'abajo'
                    If punteroy < 9 Then 'Solo bajo si no estoy abajo del todo, si no puedo bajar se descuenta la iteración
                        punteroy = punteroy + 1
                    Else
                        n = n - 1
                    End If
                Case 2 'izquierda'
                    If punterox > 0 Then 'Solo me muevo si no estoy a la izquierda de todo, si no, se descuenta la iteración
                        punterox = punterox - 1
                    Else
                        n = n - 1
                    End If
                Case 3 'derecha' 
                    If punterox < 9 Then 'Solo me muevo si no estoy a la derecha de todo, si no, se descuenta la iteración
                        punterox = punterox + 1
                    Else
                        n = n - 1
                    End If
            End Select
            Terreno(punterox, punteroy) = 1 ' Vacía la nueva ubicación
        Next n
        Terreno(punterox, punteroy) = 2 'En el último hueco ubicamos el AGV (2)
        agvx = punterox 'Guardo la cordenada X e Y del AGV
        agvy = punteroy
        Redibuja_terreno() 'Actualizamos con los nuevos cambios
        Comprueba_sensores() 'Verifico el estado de los sensores por si estoy junto a un ostáculo o el borde.


    End Sub

    'Subir
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Esta es una de las cuatro funciones de movimiento, 
        'si el movimiento es posible, actualiza la posición del AGV i redibuja la nueva situación
        If PuedoMovermeA("S") Then
            Terreno(agvx, agvy) = 1
            agvy = agvy - 1
            Terreno(agvx, agvy) = 2
            Redibuja_terreno()
            Comprueba_sensores()
        End If

    End Sub

    'Derecha
    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Esta es una de las cuatro funciones de movimiento, 
        'si el movimiento es posible, actualiza la posición del AGV i redibuja la nueva situación       
        If PuedoMovermeA("D") Then
            Terreno(agvx, agvy) = 1
            agvx = agvx + 1
            Terreno(agvx, agvy) = 2
            Redibuja_terreno()
            Comprueba_sensores()
        End If
    End Sub

    'Bajar
    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Esta es una de las cuatro funciones de movimiento, 
        'si el movimiento es posible, actualiza la posición del AGV i redibuja la nueva situación       
        If PuedoMovermeA("B") Then
            Terreno(agvx, agvy) = 1
            agvy = agvy + 1
            Terreno(agvx, agvy) = 2
            Redibuja_terreno()
            Comprueba_sensores()
        End If
    End Sub

    'Izquierda
    Private Sub ButtonI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Esta es una de las cuatro funciones de movimiento, 
        'si el movimiento es posible, actualiza la posición del AGV i redibuja la nueva situación      
        If PuedoMovermeA("I") Then
            Terreno(agvx, agvy) = 1
            agvx = agvx - 1
            Terreno(agvx, agvy) = 2
            Redibuja_terreno()
            Comprueba_sensores()
        End If
    End Sub

    'Esta función verifica si el movimento que se quiere hacer está permitido (retorna True si se puede mover y False si no)
    'La notación es     'I'--> Izquierda
    '                   'D'--> Derecha
    '                   'S'--> Subir
    '                   'B'--> Bajar
    Private Function PuedoMovermeA(ByVal direccion As String) As Boolean
        Dim puedo As Boolean
        puedo = False


        Select Case direccion
            Case "I"
                'sensor izquierda
                If agvx = 0 Then
                    puedo = False
                ElseIf Terreno(agvx - 1, agvy) = 0 Then
                    puedo = False
                Else
                    puedo = True
                End If
            Case "D"
                'sensor derecha
                If agvx = 9 Then
                    puedo = False
                ElseIf Terreno(agvx + 1, agvy) = 0 Then
                    puedo = False
                Else
                    puedo = True
                End If
            Case "S"
                'sensor arriba
                If agvy = 0 Then
                    puedo = False
                ElseIf Terreno(agvx, agvy - 1) = 0 Then
                    puedo = False
                Else
                    puedo = True
                End If
            Case "B"
                'sensor abajo
                If agvy = 9 Then
                    puedo = False
                ElseIf Terreno(agvx, agvy + 1) = 0 Then
                    puedo = False
                Else
                    puedo = True
                End If
            Case Else
                puedo = False
        End Select

        PuedoMovermeA = puedo

    End Function
    ' recepció de dades
    Private Sub Serial_DataReceived(ByVal sender As Object, ByVal e As System.IO.Ports.SerialDataReceivedEventArgs) Handles SerialPort1.DataReceived


        If SerialPort1.IsOpen Then
            Try
                rebre = SerialPort1.ReadLine()
                WriteLine(enviar)
            Catch ex As Exception
                MsgBox("Error")
            End Try
        End If


    End Sub


    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        ' connecta el port sèrie
        If Form2. <> "" And Form2. <> "" And Form2. = ""  Then
            If Not (SerialPort1.IsOpen) Then
                Try
                    With SerialPort1
                        .BaudRate = Form2.
                        .Parity = Form2.
                        .DataBits = 8
                        .StopBits = StopBits.One
                        .PortName = Form2.
                        .Handshake = Handshake.None
                    End With

                    SerialPort1.Open()
                    Button3.Text = "Desconnectar"
                    Button3.ForeColor = System.Drawing.Color.Green
                Catch ex As Exception
                    MessageBox.Show(ex.ToString())
                End Try
            Else
                Try
                    SerialPort1.Close()
                    Button3.Text = "Connectar"
                    Button3.ForeColor = System.Drawing.Color.Red
                Catch ex As Exception
                    MessageBox.Show(ex.ToString())
                End Try
            End If
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        SerialPort1.Close()
        Me.Visible = False
        form_2 = New Form2
        form_2.ShowDialog()
    End Sub

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

    End Sub
End Class
