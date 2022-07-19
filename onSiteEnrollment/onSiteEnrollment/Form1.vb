Imports MySql.Data.MySqlClient
Imports System.IO
Imports System.Runtime.InteropServices

Public Class Form1
    'Declaring DB-related variables
    Dim str As String = "server=localhost; uid=root; pwd=; database=onlineenrollment"
    Dim con As New MySqlConnection(str)

    'Declaring variables that will hold the files of the student
    Dim freshFiles() As String = {"", "", "", ""}
    Dim freshFilesNames() As String = {"PICTURE", "BIRTH CERTIFICATE", "GOOD MORAL CERTIFICATE", "FORM 138"}
    Dim Y24Files() As String = {"", ""}
    Dim Y24FilesNames() As String = {"OVRF", "SSOG"}
    Dim transFiles() As String = {"", "", "", "", ""}
    Dim transFilesNames() As String = {"PICTURE", "BIRTH CERTIFICATE", "GOOD MORAL CERTIFICATE", "HONORABLE DISMISSAL", "TRANSCRIPT OF RECORDS"}

    'Casting Shadow to the Form
    Private Const CS_DROPSHADOW As Integer = 131072
    Protected Overrides ReadOnly Property CreateParams() As System.Windows.Forms.CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ClassStyle = cp.ClassStyle Or CS_DROPSHADOW
            Return cp
        End Get
    End Property

    'Clear the Form
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

    'Enable (or disable, depend on what boolean is passed) the buttons
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

    'Show buttons which are related to 'Transferee' student
    Sub showTrans(ByVal boo As Boolean)
        Button2.Visible = boo
        Button3.Visible = boo
        Button4.Visible = boo
        Button5.Visible = boo
        Button6.Visible = boo
        clear()
    End Sub

    'Show buttons which are related to 'Freshmen' student
    Sub showFresh(ByVal boo As Boolean)
        Button7.Visible = boo
        Button8.Visible = boo
        Button9.Visible = boo
        Button10.Visible = boo
        clear()
    End Sub

    'Show buttons which are related to '2nd-4th Year' student 
    Sub showY24(ByVal boo As Boolean)
        Button12.Visible = boo
        Button13.Visible = boo
        clear()
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'Check if the user is already in enrolled.txt
        'if he is, display 'You have already enrolled!'
        'and do nothing
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

        'Get confirmation from the user to review their input
        Dim confirm As MsgBoxResult = MsgBox("Are you sure about all the information you provided?" & Environment.NewLine & "If so, please check all informations and click [yes]", MsgBoxStyle.YesNo)
        If confirm = MsgBoxResult.Yes Then

            Try
                Dim path As String
                Dim toDelete As String
                Dim possible() As String = {"2nd-4th Year", "Freshmen", "Transferee"}

                'Check for the middlename if it's blank or not and delete folders and create folder base on it.
                'This will delete all his previous records, if he is enrolling for new year.
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

                'Creating an adapter to get the student info
                If MI.Text.ToUpper = "NONE" Then
                    adapter = New MySqlDataAdapter($"SELECT * FROM students WHERE firstname='{Trim(FName.Text)}' and lastname='{Trim(LName.Text)}'", con)
                Else
                    adapter = New MySqlDataAdapter($"SELECT * FROM students WHERE firstname='{Trim(FName.Text)}' and lastname='{Trim(LName.Text)}' and middlename='{Trim(MI.Text)}'", con)
                End If

                Dim ds As New DataSet
                Dim cmd As MySqlCommand
                Dim Syear As Integer
                cmd = con.CreateCommand()
                adapter.Fill(ds)

                'Check the type of user and perform actions based on it
                If ComboBox2.Text = "Freshmen" Then
                    cmd.CommandText = $"INSERT INTO students(studentnum, firstname, lastname, middlename, address, email, number, year, accpass, course, gender) VALUES(@studentnum, @firstname, @lastname, @middlename, @address, @email, @number, '1', @accpass, @course, '{Trim(ComboBox4.Text)}')"
                    cmd.Parameters.AddWithValue("@course", ComboBox1.SelectedItem.ToString())

                ElseIf ComboBox2.Text = "2nd-4th Year" Then
                    Syear = Integer.Parse(ds.Tables(0).Rows(0)(9).ToString()) + 1
                    cmd.CommandText = $"UPDATE students SET studentnum=@studentnum, firstname=@firstname, lastname=@lastname, middlename=@middlename, address=@address, email=@email, number=@number, ispaid=False, year={Syear.ToString()}, section='', accpass=@accpass, checked=0 WHERE studentnum='{Trim(TextBox1.Text)}';"

                ElseIf ComboBox2.Text = "Transferee" Then
                    cmd.CommandText = $"INSERT INTO students(studentnum, firstname, lastname, middlename, address, email, number, year, accpass, course, gender) VALUES(@studentnum, @firstname, @lastname, @middlename, @address, @email, @number, '{ComboBox3.Text.Substring(0, 1)}', @accpass, @course, '{Trim(ComboBox4.Text)}')"
                    cmd.Parameters.AddWithValue("@course", ComboBox1.SelectedItem.ToString())

                End If

                'Get the current student count, and update it. We will also use it for the creation of student number
                Dim studentCount As Integer = getStudentCount() + 1
                updateStudentCount(studentCount)

                'Variables which will be used to create a student number 
                Dim year = Date.Today.Year()
                Dim zeros As Integer = 5 - CStr(getStudentCount()).Length
                Dim zeroStr As String = StrDup(zeros, "0")

                'Student number = {last 2 digit of year}-{zeros depending on the length of current student count}{student count}
                Dim studentNum As String = CStr(year).Substring(2) & "-" & zeroStr & studentCount.ToString()

                'Password for the student account
                Dim password = getRandomPassword()

                'Adding parameters for the SQL command
                cmd.Parameters.AddWithValue("@studentnum", studentNum)
                cmd.Parameters.AddWithValue("@firstname", Trim(FName.Text))
                cmd.Parameters.AddWithValue("@lastname", Trim(LName.Text))

                'Adding value to sql code depends on what the middle name of the user
                If MI.Text.ToUpper = "NONE" Then
                    cmd.Parameters.AddWithValue("@middlename", "")
                Else
                    cmd.Parameters.AddWithValue("@middlename", Trim(MI.Text))
                End If
                cmd.Parameters.AddWithValue("@address", Trim(Address.Text))
                cmd.Parameters.AddWithValue("@email", Trim(Email.Text))
                cmd.Parameters.AddWithValue("@number", Trim(Number.Text))
                cmd.Parameters.AddWithValue("@accpass", password)

                'Execute the Query
                cmd.ExecuteNonQuery()
                con.Close()
                MsgBox("Submition Success!")

                'Updating the enrolled student to be checked later
                If MI.Text.ToUpper = "NONE" Then
                    updateEnrolled($"{Trim(FName.Text)} {Trim(LName.Text)}")
                Else
                    updateEnrolled($"{Trim(FName.Text)} {Trim(LName.Text)} {Trim(MI.Text)}")
                End If

                'Clear the Form
                clear()
            Catch ex As Exception
                MsgBox(ex.Message)
                MsgBox(ex.ToString)
            Finally
                'Close MySQL connection and restart the application every time
                'a process is done
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

    'Drop choices when the user clicked a ComboBox
    Private Sub ComboBox1_Click(sender As Object, e As EventArgs) Handles ComboBox1.Click, ComboBox2.Click, ComboBox3.Click, ComboBox4.Click
        sender.DroppedDown = True
    End Sub

    Private Sub ComboBox2_TextChanged(sender As Object, e As EventArgs) Handles ComboBox2.TextChanged
        enableAll(True)

        'Disabling visible and enabled buttons based on what will the user enrolled as
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

    'Getting required files from the user
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

    'Get the picture of the student
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

    'Check if the student number exist in the database
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Dim adapter As MySqlDataAdapter
        Dim ds As New DataSet
        If TextBox1.Focused Then
            Try
                con.Open()
                adapter = New MySqlDataAdapter($"SELECT studentnum FROM students WHERE studentnum LIKE '%{TextBox1.Text}%'", con)
                adapter.Fill(ds)
                Dim s = ds.Tables(0).Rows(0)(0).ToString()
                If s.Length > 0 Then
                    'Change this, there can be enrollees which are incoming 2nd-4th year but are not transferee or from the same school
                    enableAll(True)
                    ComboBox1.Enabled = False
                End If
            Catch ex As Exception
                enableAll(False)
            Finally
                con.Close()

            End Try
        End If

    End Sub

    'From here to bottom are all just design-related and responsive codes
    <DllImport("user32.DLL", EntryPoint:="ReleaseCapture")>
    Private Shared Sub ReleaseCapture()
    End Sub
    <DllImport("user32.DLL", EntryPoint:="SendMessage")>
    Private Shared Sub SendMessage(ByVal hWnd As System.IntPtr, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer)
    End Sub

    Private Sub Button1_MouseHover(sender As Object, e As EventArgs) Handles Button1.MouseHover
        If Button1.Enabled Then
            Button1.BackColor = Color.Orange
        End If
    End Sub

    Private Sub Button1_MouseLeave(sender As Object, e As EventArgs) Handles Button1.MouseLeave
        If Button1.Enabled Then
            Button1.BackColor = Color.DodgerBlue
        End If
    End Sub

    Private Sub Form1_MouseDown(sender As Object, e As MouseEventArgs) Handles MyBase.MouseDown, Label12.MouseDown, Panel1.MouseDown
        ReleaseCapture()
        SendMessage(Me.Handle, &H112&, &HF012&, 0)
    End Sub

    Private Sub IconButton3_Click(sender As Object, e As EventArgs) Handles IconButton3.Click
        Application.Exit()
    End Sub

    Private Sub IconButton1_Click(sender As Object, e As EventArgs) Handles IconButton1.Click
        WindowState = FormWindowState.Minimized
    End Sub

    Private Sub IconButton3_MouseHover(sender As Object, e As EventArgs) Handles IconButton3.MouseHover, IconButton1.MouseHover
        If sender Is IconButton1 Then
            sender.IconColor = Color.Gray
        Else
            sender.IconColor = Color.Red
        End If
    End Sub

    Private Sub IconButton3_MouseLeave(sender As Object, e As EventArgs) Handles IconButton3.MouseLeave, IconButton1.MouseLeave
        sender.IconColor = Color.White
    End Sub
End Class
