Imports MySql.Data.MySqlClient
Imports MySql.Data
Module ModuleKoneksi
    Public conn As MySqlConnection
    Public cmd As MySqlCommand
    Public ds As DataSet
    Public da As MySqlDataAdapter
    Public rd As MySqlDataReader
    Public bs As BindingSource
    Public lokasidb As String
    Public Sub ambilkoneksi()
        lokasidb = "server=localhost;user id=root; password= ;database=db_tbai"
        conn = New MySqlConnection(lokasidb)
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
    End Sub
End Module
