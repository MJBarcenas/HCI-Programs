Imports MySql.Data.MySqlClient
Imports Microsoft.Office.Interop.Word
Imports System.Runtime.InteropServices
Public Class Form1
    Dim studentNum As String
    Dim password As String
    Dim fname As String
    Dim lname As String
    Dim MI As String
    Sub loadData()
        Try
            Dim query As String = "select * from students where ispaid=1 and section=''"
            Dim adpt As New MySqlDataAdapter(query, con)
            Dim ds As New DataSet()
            adpt.Fill(ds)
            DataGridView1.DataSource = ds.Tables(0)
            con.Close()
            TextBox1.Clear()
            TextBox3.Clear()
            TextBox4.Clear()
            TextBox6.Clear()
        Catch ex As MySql.Data.MySqlClient.MySqlException
            MsgBox("Cannot connect to database!" & Environment.NewLine & "Please make sure you started the database before you open this appliction!" & Environment.NewLine & "Application will exit now...", Title:="ERROR")
            Me.Close()
        End Try

    End Sub
    Sub disableButtons()
        Button1.Enabled = False
        Button2.Enabled = False
        Button3.Enabled = False
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        disableButtons()
        loadData()
        TextBox1.ReadOnly() = True
        TextBox4.ReadOnly() = True
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
        If TextBox3.Text.Length() = 0 Then
            loadData()
            disableButtons()
            Return
        End If
        Dim adapter As MySqlDataAdapter
        Dim ds As New DataSet
        Try
            con.Open()
            adapter = New MySqlDataAdapter($"select * from students where firstname Like '%{TextBox3.Text}%' or lastname like '%{TextBox3.Text}%' and ispaid=True and section=''", con)
            con.Close()
            adapter.Fill(ds)
            DataGridView1.DataSource = ds.Tables(0)
            con.Close()
            If DataGridView1.RowCount() = 1 Then
                Dim row As DataGridViewRow = DataGridView1.Rows(0)
                Try
                    TextBox1.Text = $"{row.Cells(2).Value.ToString()}, {row.Cells(1).Value.ToString()} {row.Cells(3).Value.ToString()}"
                    TextBox6.Text = row.Cells(5).Value.ToString()
                    studentNum = row.Cells(0).Value.ToString()
                    password = row.Cells(9).Value.ToString()
                    fname = row.Cells(1).Value.ToString()
                    lname = row.Cells(2).Value.ToString()
                    MI = row.Cells(3).Value.ToString()
                    Button1.Enabled() = True
                Catch ex As Exception

                End Try
            Else
                TextBox1.Clear()
                TextBox4.Clear()
                TextBox6.Clear()
                disableButtons()
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim adapter As MySqlDataAdapter
        Dim ds As New DataSet
        Try
            con.Open()
            adapter = New MySqlDataAdapter("select * from classes where isfull=0", con)
            con.Close()
            adapter.Fill(ds)
            Dim arr As String() = (From myRow In ds.Tables(0).AsEnumerable
                                   Select myRow.Field(Of String)("section")).ToArray
            Dim len As Integer = arr.Length()
            Dim randomSection As Integer = GetRandom(0, len)
            TextBox4.Text = arr(randomSection)
            TextBox4.ReadOnly() = False
            Button2.Enabled = True
            Button3.Enabled = True

        Catch ex As Exception

        End Try
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Dim row As DataGridViewRow = DataGridView1.CurrentRow
        Try
            TextBox1.Text = $"{row.Cells(2).Value.ToString()}, {row.Cells(1).Value.ToString()} {row.Cells(3).Value.ToString()}"
            TextBox6.Text = row.Cells(5).Value.ToString()
            studentNum = row.Cells(0).Value.ToString()
            password = row.Cells(9).Value.ToString()
            fname = row.Cells(1).Value.ToString()
            lname = row.Cells(2).Value.ToString()
            MI = row.Cells(3).Value.ToString()
            Button1.Enabled() = True
            TextBox4.ReadOnly() = False
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged, TextBox6.TextChanged
        If TextBox4.Text.Length() = 2 Then
            Dim sections As String() = getSections()
            If sections.Contains(TextBox4.Text.ToUpper()) Then
                Button1.Enabled = True
                Button2.Enabled = True
                Button3.Enabled = True
            End If
        ElseIf TextBox4.Text.Length() = 0 Then
            Button1.Enabled = True
        Else
            disableButtons()
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        loadData()
        disableButtons()
        TextBox4.ReadOnly() = True
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim section As String = TextBox4.Text.ToUpper()
        Dim cmd As MySqlCommand
        Dim cmd2 As MySqlCommand

        Dim adapter As MySqlDataAdapter
        Dim ds As New DataSet


        Try
            con.Open()
            adapter = New MySqlDataAdapter("select isfull, studentcount from classes where section='" & section & "'", con)
            adapter.Fill(ds)

            Dim isFull As Boolean = Boolean.Parse(ds.Tables(0).Rows(0)(0).ToString())
            Dim count As Integer = Integer.Parse(ds.Tables(0).Rows(0)(1).ToString())


            count = count + 1
            If count >= 40 Then
                isFull = True
            Else
                isFull = False
            End If

            cmd = con.CreateCommand()
            cmd.CommandText = "update classes set studentcount=@studentcount, isfull=@isfull where section=@section"
            cmd.Parameters.AddWithValue("@studentcount", count)
            cmd.Parameters.AddWithValue("@isfull", isFull)
            cmd.Parameters.AddWithValue("@section", section)
            cmd.ExecuteNonQuery()
            con.Close()

            con.Open()
            cmd2 = con.CreateCommand()
            cmd2.CommandText = $"update students set section=@section where studentnum='{studentNum}'"
            cmd2.Parameters.AddWithValue("@section", TextBox4.Text)
            cmd2.ExecuteNonQuery()
            con.Close()
            generateOVRF(fname, lname, MI, studentNum, TextBox6.Text)
            Dim mail = "Congratulations " & TextBox1.Text & "!!!" & Environment.NewLine & "You are now enrolled at Colegio de Montalban" & Environment.NewLine & "Here are your copy of OVRF and credentials to login in the Colegio de Montalban Application." & Environment.NewLine & "Student Number: " & studentNum & Environment.NewLine & "Password: " & password
            sendEmail(mail, "mjbrcns51@gmail.com", "tkzoblulgmuleflh", TextBox6.Text, $"D:\School\3rd Year - Second Semester [3I]\Human Computer Interaction\Project\Programs\Registration\Registration\OVRFs\{studentNum} {lname}, {fname} {MI}.pdf")
            loadData()
            disableButtons()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        con.Close()

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        sendEmail("here", "mjbrcns51@gmail.com", "tkzoblulgmuleflh", "imaqtchael@gmail.com", "D:\School\3rd Year - Second Semester [3I]\Human Computer Interaction\Project\Programs\Registration\Registration\pig.pdf")

    End Sub
End Class
