Imports MySql.Data.MySqlClient
Imports System.IO
Public Class Form1
    Dim str As String = "server=localhost; uid=root; pwd=; database=onlineenrollment"
    Dim con As New MySqlConnection(str)
    Dim freshFiles() As String = {"", "", "", ""}
    Dim freshFilesNames() As String = {"PICTURE", "BIRTH CERTIFICATE", "GOOD MORAL CERTIFICATE", "FORM 138"}
    Dim Y24Files() As String = {"", ""}
    Dim Y24FilesNames() As String = {"OVRF", "SSOG"}
    Dim transFiles() As String = {"", "", "", "", ""}
    Dim transFilesNames() As String = {"PICTURE", "BIRTH CERTIFICATE", "GOOD MORAL CERTIFICATE", "HONORABLE DISMISSAL", "TRANSCRIPT OF RECORDS"}
    Sub clear()
        LName.Clear()
        FName.Clear()
        MI.Clear()
        Address.Clear()
        Email.Clear()
        Number.Clear()
        ComboBox1.Text = ""
        ComboBox3.Text = ""
        ComboBox4.Text = ""
        TextBox1.Clear()
    End Sub

    Sub enableAll(ByVal boo As Boolean)
        LName.Enabled = boo
        FName.Enabled = boo
        MI.Enabled = boo
        Address.Enabled = boo
        Email.Enabled = boo
        Number.Enabled = boo
        ComboBox1.Enabled = boo
        Button1.Enabled = boo
    End Sub

    Sub showTrans(ByVal boo As Boolean)
        Button2.Visible = boo
        Button3.Visible = boo
        Button4.Visible = boo
        Button5.Visible = boo
        Button6.Visible = boo
        clear()
    End Sub

    Sub showFresh(ByVal boo As Boolean)
        Button7.Visible = boo
        Button8.Visible = boo
        Button9.Visible = boo
        Button10.Visible = boo
        clear()
    End Sub

    Sub showY24(ByVal boo As Boolean)
        Button12.Visible = boo
        Button13.Visible = boo
        clear()
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If MI.Text.ToUpper = "NONE" Then
            If ifEnrolled($"{Trim(FName.Text)} {Trim(LName.Text)}") Then
                MsgBox("You have already enrolled!")
                Return
            End If
        Else
            If ifEnrolled($"{Trim(FName.Text)} {Trim(LName.Text)} {Trim(MI.Text)}") Then
                MsgBox("You have already enrolled!")
                Return
            End If
        End If
        'Check for empty fields on the code
        Dim emptyTextBoxes =
            From txt In Me.Controls.OfType(Of TextBox)()
            Where txt.Text.Length = 0 And txt.Visible And txt.Enabled
            Select txt.Name
        If emptyTextBoxes.Any Then
            MessageBox.Show("Please fill up all the fields")
            Return
        End If

        emptyTextBoxes =
            From txt In Me.Controls.OfType(Of ComboBox)()
            Where txt.Text.Length = 0 And txt.Visible And txt.Enabled
            Select txt.Name
        If emptyTextBoxes.Any Then
            MessageBox.Show("Please fill up all the fields")
            Return
        End If

        'Get confirmation from the user
        Dim confirm As MsgBoxResult = MsgBox("Are you sure about all the information you provided?" & Environment.NewLine & "If so, please check all informations and click [yes]", MsgBoxStyle.YesNo)
        If confirm = MsgBoxResult.Yes Then

            Try
                Dim path As String
                Dim toDelete As String
                Dim possible() As String = {"2nd-4th Year", "Freshmen", "Transferee"}
                'Check for the middlename if it's blank or not and delete folders and create folder base on it
                If MI.Text.ToUpper = "NONE" Then
                    For i As Integer = 0 To possible.Length - 1
                        toDelete = $"D:\Programming\Programs\uploads\{possible(i)}\{Trim(LName.Text)}, {Trim(FName.Text)}"
                        If System.IO.Directory.Exists(toDelete) Then
                            System.IO.Directory.Delete(toDelete, True)
                        End If
                    Next
                    path = System.IO.Directory.CreateDirectory($"D:\Programming\Programs\uploads\{Trim(ComboBox2.Text)}\{Trim(LName.Text)}, {Trim(FName.Text)}").ToString()
                Else
                    For i As Integer = 0 To possible.Length - 1
                        toDelete = $"D:\Programming\Programs\uploads\{possible(i)}\{Trim(LName.Text)}, {Trim(FName.Text)}-{Trim(MI.Text)}"
                        If System.IO.Directory.Exists(toDelete) Then
                            System.IO.Directory.Delete(toDelete, True)
                        End If
                    Next
                    path = System.IO.Directory.CreateDirectory($"D:\Programming\Programs\uploads\{Trim(ComboBox2.Text)}\{Trim(LName.Text)}, {Trim(FName.Text)}-{Trim(MI.Text)}").ToString()
                End If
                'Check for the type of enrollment
                If ComboBox2.Text = "2nd-4th Year" Then
                    'Saving the uploaded files on the created folder
                    For i As Integer = 0 To Y24Files.Length - 1
                        Dim arr() As String = Split(Y24Files(i), "\")
                        Dim file As String = arr(arr.Length - 1)
                        saveFile(Y24Files(i), $"{path}\[{Y24FilesNames(i)}] {file}")
                    Next
                ElseIf ComboBox2.Text = "Transferee" Then
                    'Saving the uploaded files on the created folder
                    For i As Integer = 0 To transFiles.Length - 1
                        Dim arr() As String = Split(transFiles(i), "\")
                        Dim file As String = arr(arr.Length - 1)
                        saveFile(transFiles(i), $"{path}\[{transFilesNames(i)}] {file}")
                    Next
                ElseIf ComboBox2.Text = "Freshmen" Then
                    'Saving the uploaded files on the created folder
                    For i As Integer = 0 To freshFiles.Length - 1
                        Dim arr() As String = Split(freshFiles(i), "\")
                        Dim file As String = arr(arr.Length - 1)
                        saveFile(freshFiles(i), $"{path}\[{freshFilesNames(i)}] {file}")
                    Next
                End If
                con.Open()
                Dim adapter As MySqlDataAdapter
                If MI.Text.ToUpper = "NONE" Then
                    adapter = New MySqlDataAdapter($"select * from students where firstname='{Trim(FName.Text)}' and lastname='{Trim(LName.Text)}'", con)
                Else
                    adapter = New MySqlDataAdapter($"select * from students where firstname='{Trim(FName.Text)}' and lastname='{Trim(LName.Text)}' and middlename='{Trim(MI.Text)}'", con)
                End If

                Dim ds As New DataSet
                Dim cmd As MySqlCommand
                Dim Syear As Integer
                cmd = con.CreateCommand()
                adapter.Fill(ds)

                If ComboBox2.Text = "Freshmen" Then
                    If ds.Tables(0).Rows.Count >= 1 Then
                        MsgBox("You already submitted a requirements!")
                        con.Close()
                        Return
                    Else
                        cmd.CommandText = $"insert into students(studentnum, firstname, lastname, middlename, address, email, number, year, accpass, course, gender) values(@studentnum, @firstname, @lastname, @middlename, @address, @email, @number, '1', @accpass, @course, '{Trim(ComboBox4.Text)}')"
                        cmd.Parameters.AddWithValue("@course", ComboBox1.SelectedItem.ToString())
                    End If
                ElseIf ComboBox2.Text = "2nd-4th Year" Then
                    If ds.Tables(0).Rows.Count = 0 Then
                        MsgBox("You are not enrolled as a student in Colegion de Montalban")
                        con.Close()
                        Return
                    Else
                        Syear = Integer.Parse(ds.Tables(0).Rows(0)(9).ToString()) + 1
                        cmd.CommandText = $"update students set studentnum=@studentnum, firstname=@firstname, lastname=@lastname, middlename=@middlename, address=@address, email=@email, number=@number, ispaid=False, year={Syear.ToString()}, section='', accpass=@accpass where studentnum='{Trim(TextBox1.Text)}';"
                    End If
                ElseIf ComboBox2.Text = "Transferee" Then
                    If ds.Tables(0).Rows.Count >= 1 Then
                        MsgBox("You already submitted a requirements!")
                        con.Close()
                        Return
                    Else
                        cmd.CommandText = $"insert into students(studentnum, firstname, lastname, middlename, address, email, number, year, accpass, course, gender) values(@studentnum, @firstname, @lastname, @middlename, @address, @email, @number, '{ComboBox3.Text.Substring(0, 1)}', @accpass, @course, '{Trim(ComboBox4.Text)}')"
                        cmd.Parameters.AddWithValue("@course", ComboBox1.SelectedItem.ToString())
                    End If
                End If

                Dim studentCount As Integer = getStudentCount() + 1
                updateStudentCount(studentCount)

                Dim year = Date.Today.Year()
                Dim zeros As Integer = 5 - CStr(getStudentCount()).Length
                Dim zeroStr As String = StrDup(zeros, "0")
                Dim studentNum As String = CStr(year).Substring(2) & "-" & zeroStr & studentCount.ToString()

                Dim password = getRandomPassword()

                cmd.Parameters.AddWithValue("@studentnum", studentNum)
                cmd.Parameters.AddWithValue("@firstname", Trim(FName.Text))
                cmd.Parameters.AddWithValue("@lastname", Trim(LName.Text))
                If MI.Text.ToUpper = "NONE" Then
                    cmd.Parameters.AddWithValue("@middlename", "")
                Else
                    cmd.Parameters.AddWithValue("@middlename", Trim(MI.Text))
                End If
                cmd.Parameters.AddWithValue("@address", Trim(Address.Text))
                cmd.Parameters.AddWithValue("@email", Trim(Email.Text))
                cmd.Parameters.AddWithValue("@number", Trim(Number.Text))
                cmd.Parameters.AddWithValue("@accpass", password)

                cmd.ExecuteNonQuery()
                con.Close()
                MsgBox("Submition Success!")
                If MI.Text.ToUpper = "NONE" Then
                    updateEnrolled($"{Trim(FName.Text)} {Trim(LName.Text)}")
                Else
                    updateEnrolled($"{Trim(FName.Text)} {Trim(LName.Text)} {Trim(MI.Text)}")
                End If

                clear()
            Catch ex As Exception
                MsgBox(ex.Message)
                MsgBox(ex.ToString)
            Finally
                con.Close()
                Application.Restart()
                'enableAll(False)
                'clear()
                'freshFiles = {"", "", "", ""}
                'Y24Files = {"", ""}
                'transFiles = {"", "", "", "", ""}
                'TextBox1.Enabled = False
                'ComboBox3.Enabled = False
                'showFresh(False)
                'showTrans(False)
                'showY24(False)

            End Try
        End If
    End Sub

    Private Sub ComboBox1_Click(sender As Object, e As EventArgs) Handles ComboBox1.Click, ComboBox2.Click, ComboBox3.Click, ComboBox4.Click
        sender.DroppedDown = True
    End Sub

    Private Sub ComboBox2_TextChanged(sender As Object, e As EventArgs) Handles ComboBox2.TextChanged
        enableAll(True)
        If ComboBox2.Text = "2nd-4th Year" Then
            TextBox1.Visible = True
            Label9.Visible = True
            TextBox1.Enabled = True
            Label9.Enabled = True
            ComboBox1.Enabled = False
            Label10.Visible = False
            ComboBox3.Visible = False
            showY24(True)
            showTrans(False)
            showFresh(False)
            ComboBox4.Visible = False
            Label11.Visible = False
        ElseIf ComboBox2.Text = "Transferee" Then
            Label10.Visible = True
            ComboBox3.Visible = True
            Label10.Enabled = True
            ComboBox3.Enabled = True
            TextBox1.Visible = False
            Label9.Visible = False
            ComboBox1.Enabled = True
            showY24(False)
            showTrans(True)
            showFresh(False)
            ComboBox4.Visible = True
            Label11.Visible = True
        ElseIf ComboBox2.Text = "Freshmen" Then
            TextBox1.Visible = False
            Label9.Visible = False
            ComboBox1.Enabled = True
            Label10.Visible = False
            ComboBox3.Visible = False
            showY24(False)
            showTrans(False)
            showFresh(True)
            ComboBox4.Visible = True
            Label11.Visible = True
        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button9.Click, Button8.Click, Button6.Click, Button5.Click, Button4.Click, Button3.Click, Button13.Click, Button12.Click, Button10.Click
        If sender Is Button3 Then
            transFiles(1) = getFile(sender, "PDF|*.pdf")
        ElseIf sender Is Button4 Then
            transFiles(2) = getFile(sender, "PDF|*.pdf")
        ElseIf sender Is Button5 Then
            transFiles(3) = getFile(sender, "PDF|*.pdf")
        ElseIf sender Is Button6 Then
            transFiles(4) = getFile(sender, "PDF|*.pdf")
        ElseIf sender Is Button8 Then
            freshFiles(1) = getFile(sender, "PDF|*.pdf")
        ElseIf sender Is Button9 Then
            freshFiles(2) = getFile(sender, "PDF|*.pdf")
        ElseIf sender Is Button10 Then
            freshFiles(3) = getFile(sender, "PDF|*.pdf")
        ElseIf sender Is Button12 Then
            Y24Files(0) = getFile(sender, "PDF|*.pdf")
        ElseIf sender Is Button13 Then
            Y24Files(1) = getFile(sender, "PDF|*.pdf")
        End If
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button2.Click, Button7.Click
        If sender Is Button2 Then
            transFiles(0) = getFile(sender, "Bitmap|*.bmp|JPEG|*.jpg,*.jpeg|PNG|*.png|GIF|*.gif")
            'Dim x As String = System.IO.Directory.CreateDirectory($"C:\Program Files\xampp\htdocs\Programs\uploads\2nd-4th Year\{LName.Text}, {FName.Text} {MI.Text}").ToString
            'Dim arr() As String = Split(transFiles(0), "\")
            'Dim file As String = arr(arr.Length - 1)
            'saveFile(transFiles(0), $"D:\{file}")
        ElseIf sender Is Button7 Then
            freshFiles(0) = getFile(sender, "Bitmap|*.bmp|JPEG|*.jpg,*.jpeg|PNG|*.png|GIF|*.gif")
        End If
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        MsgBox(ifEnrolled("Michael Justin Barcenas"))
    End Sub
End Class
