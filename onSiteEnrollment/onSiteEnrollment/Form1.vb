Imports MySql.Data.MySqlClient
Public Class Form1
    Dim str As String = "server=localhost; uid=root; pwd=; database=onlineenrollment"
    Dim con As New MySqlConnection(str)

    Sub clear()
        LName.Clear()
        FName.Clear()
        MI.Clear()
        Address.Clear()
        Email.Clear()
        Number.Clear()
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim emptyTextBoxes =
            From txt In Me.Controls.OfType(Of TextBox)()
            Where txt.Text.Length = 0
            Select txt.Name
        If emptyTextBoxes.Any Then
            MessageBox.Show(String.Format("Please fill following textboxes: {0}",
                    String.Join(", ", emptyTextBoxes)))
            Return
        End If

        Dim confirm As MsgBoxResult = MsgBox("Are you sure about all the information you provided?" & Environment.NewLine & "If so, please checkk all informations and click [yes]", MsgBoxStyle.YesNo)
        If confirm = MsgBoxResult.Yes Then
            Dim cmd As MySqlCommand
            Try
                con.Open()

                Dim adapter As MySqlDataAdapter = New MySqlDataAdapter($"select * from students where firstname='{FName.Text}' and lastname='{LName.Text}' and middlename='{MI.Text}'", con)
                Dim ds As New DataSet
                adapter.Fill(ds)

                If ds.Tables(0).Rows.Count >= 1 Then
                    MsgBox("You already submitted a requirements!")
                    Return
                End If

                Dim studentCount As Integer = getStudentCount() + 1
                updateStudentCount(studentCount)

                Dim year = Date.Today.Year()
                Dim zeros As Integer = 5 - CStr(getStudentCount()).Length
                Dim zeroStr As String = StrDup(zeros, "0")
                Dim studentNum As String = CStr(year).Substring(2) & "-" & zeroStr & studentCount.ToString()

                Dim password = getRandomPassword()

                cmd = con.CreateCommand()
                cmd.CommandText = "insert into students(studentnum, firstname, lastname, middlename, address, email, number, accpass) values(@studentnum, @firstname, @lastname, @middlename, @address, @email, @number, @accpass)"
                cmd.Parameters.AddWithValue("@studentnum", studentNum)
                cmd.Parameters.AddWithValue("@firstname", FName.Text)
                cmd.Parameters.AddWithValue("@lastname", LName.Text)
                If MI.Text.ToUpper = "NONE" Then
                    cmd.Parameters.AddWithValue("@middlename", "")
                Else
                    cmd.Parameters.AddWithValue("@middlename", MI.Text)
                End If
                cmd.Parameters.AddWithValue("@address", Address.Text)
                cmd.Parameters.AddWithValue("@email", Email.Text)
                cmd.Parameters.AddWithValue("@number", Number.Text)
                cmd.Parameters.AddWithValue("@accpass", password)
                cmd.ExecuteNonQuery()
                con.Close()
                MsgBox("Submition Success!")
                clear()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Else
        End If


    End Sub

End Class
