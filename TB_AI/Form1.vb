Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.IO
Imports System
Imports MySql
Public Class Form1
    Sub nomorotomatis()
        Try
            ambilkoneksi()
            Dim cmd As MySqlCommand = New MySqlCommand("select * from air order by id_air desc", conn)
            Dim dtreader As MySqlDataReader = cmd.ExecuteReader
            dtreader.Read()
            Dim id_user As String
            Try
                If Not dtreader.HasRows Then
                    id_user = "ID0001"
                Else
                    id_user = Val(Microsoft.VisualBasic.Mid(dtreader.Item("id_air").ToString, 5, 3)) + 1
                    If Len(id_user) = 1 Then
                        id_user = "ID000" & id_user & ""
                    ElseIf Len(id_user) = 2 Then
                        id_user = "ID00" & id_user & ""
                    ElseIf Len(id_user) = 3 Then
                        id_user = "ID0" & id_user & ""
                    End If
                End If
            Catch ex As Exception
                MsgBox(ex.ToString)
            Finally
                Label10.Text = id_user
                conn.Close()
            End Try
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Sub bersih()
        ' Label4.Text = "status"
        nomorotomatis()

        'TextBox1.Text = ""
        'TextBox2.Text = ""
        'TextBox3.Text = ""
        'TextBox4.Text = ""
        'TextBox5.Text = ""
        ' ComboBox1.Text = "-----PILIH-----"
        ' ComboBox2.Text = "-PILIH-"
    End Sub
   
    Sub simpandata()
        ambilkoneksi()
        If TextBox1.Text = "" Or Label4.Text = "" Then
            MsgBox("data masih kosong")
        Else
            Try
                lokasidb = "insert into air values ('" & Label10.Text & "','" & Label4.Text & "','" & Format(Now) & "')"
                cmd = New MySqlCommand(lokasidb, conn)
                cmd.ExecuteNonQuery()
                'MsgBox("Data Berhasil Disimpan")
            Catch ex As MySqlException
                MsgBox("Data Gagal Disimpan :" & ex.Message)
            Finally
                'cmd.Dispose()
                'conn.Close()
            End Try
            ' Tampil1()
            bersih()
        End If
    End Sub
    Dim WithEvents SerialPort As New IO.Ports.SerialPort
    Sub PortAvailable()

        For Each PortName As String In My.Computer.Ports.SerialPortNames

            cbPort.Items.Add(PortName)

        Next

    End Sub

    Sub boudRate()

        With cbBoudrate

            .Items.Add("9600")

            .Items.Add("115200")

        End With

    End Sub

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Call nomorotomatis()
        'Tampil1()
        Timer1.Enabled = True
        PortAvailable()
        TextBox1.Enabled = False
        Label5.BackColor = Color.Black
        boudRate()
    End Sub
    Sub ReadDataArduino()
        Try
            Dim value As Double = CDbl(SerialPort1.ReadLine())
            TextBox1.Text = (value)
            If TextBox1.Text <= 150 Then
                Label4.Text = "AIR KERUH"
                Label5.BackColor = Color.Red
                simpandata()

            Else
                Label4.Text = "AIR JERNIH"
                Label5.BackColor = Color.Green
                simpandata()
            End If

            lbInfo.Items.Add(value)
            lbInfo.SelectedIndex = lbInfo.Items.Count - 1

        Catch ex As Exception
            MsgBox("gagal")
        End Try

    End Sub
    Public Delegate Sub delegateReadData()
    Private Sub BackgroundWorker1_DoEvent(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork

        Dim exe = New delegateReadData(AddressOf ReadDataArduino)

        Me.Invoke(exe)
        'ReadDataArduino()
    End Sub

    Private Sub SerialPort1_DataReceived(ByVal sender As Object, ByVal e As IO.Ports.SerialDataReceivedEventArgs) Handles SerialPort1.DataReceived

        If Not BackgroundWorker1.IsBusy Then

            BackgroundWorker1.RunWorkerAsync()

        End If
    End Sub


    Private Sub btStart_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btStart.Click
        If btStart.Text = "Start" Then

            If cbPort.SelectedItem = Nothing Or cbBoudrate.SelectedItem = Nothing Then

                MessageBox.Show("Port/Boudrate belum dipilih, silahlah pilih sebelum START")

                Exit Sub

            End If

            Try

                If SerialPort1.IsOpen Then
                    'eadDataArduino()
                    SerialPort1.Close()

                End If

                With SerialPort1

                    .PortName = cbPort.SelectedItem.ToString

                    .BaudRate = cbBoudrate.SelectedItem.ToString

                    .DtrEnable = True

                    .RtsEnable = True

                    .Open()

                End With
                ' Timer1.Start()
                'ReadDataArduino()
                cbPort.Enabled = False

                cbBoudrate.Enabled = False

                btStart.Text = "Stop"

            Catch ex As Exception

                MsgBox("Gagal dalam konfigurasi Serial")

            End Try

        ElseIf btStart.Text = "Stop" Then

            Try

                If SerialPort1.IsOpen Then

                    SerialPort1.Close()

                End If

                cbPort.Enabled = True

                cbBoudrate.Enabled = True

                btStart.Text = "Start"

            Catch ex As Exception

                MsgBox("Gagal dalam menutup Serial")

            End Try
        End If
    End Sub



    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles Timer1.Tick
        Label6.Text = Format(Now, "dd / MMM / yyyy")
        Label7.Text = Format(Now, "HH:mm:ss")

    End Sub
End Class
