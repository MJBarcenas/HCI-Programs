Imports MySql.Data.MySqlClient
Imports Microsoft.Office.Interop.Word
Imports System.Runtime.InteropServices
Public Class Form1
    'Declaring Global variables
    Dim studentNum As String
    Dim password As String
    Dim fname As String
    Dim lname As String
    Dim MI As String
    Dim year As String
    Dim semester As String

    'Casting Shadow to the Form
    Private Const CS_DROPSHADOW As Integer = 131072
    Protected Overrides ReadOnly Property CreateParams() As System.Windows.Forms.CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ClassStyle = cp.ClassStyle Or CS_DROPSHADOW
            Return cp
        End Get
    End Property

    'Will load/refresh the data when launching and performing a register
    Sub loadData()
        Try
            con.Open()

            'Getting the rows of the student to be displayed
            Dim query As String = "select studentnum, firstname, lastname, middlename, address, email, number, ispaid, year, section, accpass from students where ispaid=1 and section='' and checked=0"
            Dim adpt As New MySqlDataAdapter(query, con)
            Dim ds As New DataSet()
            adpt.Fill(ds)

            'Putting those data to DataGridView1
            DataGridView1.DataSource = ds.Tables(0)
            con.Close()
            TextBox1.Clear()
            TextBox3.Clear()
            TextBox4.Clear()
            TextBox6.Clear()

            'Enabling the 'ENROLL ALL' when all of the students are registered
            If DataGridView1.Rows.Count = 0 Then
                Button2.Enabled = True
            End If
        Catch ex As MySql.Data.MySqlClient.MySqlException

            'Informing Admin if the program cannot make a connection to the Database and closing the program
            MsgBox("Cannot connect to database!" & Environment.NewLine & "Please make sure you started the database before you open this appliction!" & Environment.NewLine & "Application will exit now...", Title:="ERROR")
            Me.Close()
        End Try

    End Sub

    'Disabling buttons
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

        'Asking admin for the semester, this will be used to check what OVRF will the program will 
        'generate later on
        semester = InputBox("Enroll for semester: ", "Question", "1 or 2")
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
        'Reseting the DataGridView1 if there is no text on searchbox
        If TextBox3.Text.Length() = 0 Then
            loadData()
            disableButtons()
            Return
        End If
        Dim adapter As MySqlDataAdapter
        Dim ds As New DataSet
        Try
            con.Open()
            adapter = New MySqlDataAdapter($"select studentnum, firstname, lastname, middlename, address, email, number, ispaid, year, section, accpass from students where (firstname like '%{TextBox3.Text}%' or lastname like '%{TextBox3.Text}%') and ispaid=True and section=''", con)
            con.Close()
            adapter.Fill(ds)
            DataGridView1.DataSource = ds.Tables(0)
            con.Close()

            'Displaying the data on DataGridView1 when there is a result from the MySqlDataAdapter
            If DataGridView1.RowCount() = 1 Then
                Dim row As DataGridViewRow = DataGridView1.Rows(0)
                Try
                    TextBox1.Text = $"{row.Cells(2).Value.ToString()}, {row.Cells(1).Value.ToString()} {row.Cells(3).Value.ToString()}"
                    TextBox6.Text = row.Cells(5).Value.ToString()
                    studentNum = row.Cells(0).Value.ToString()
                    password = row.Cells(10).Value.ToString()
                    fname = row.Cells(1).Value.ToString()
                    lname = row.Cells(2).Value.ToString()
                    MI = row.Cells(3).Value.ToString()
                    year = row.Cells(8).Value.ToString()
                    Button1.Enabled() = True
                Catch ex As Exception

                End Try
            Else
                'Clearing TextBoxes if there is no result for the search
                TextBox1.Clear()
                TextBox4.Clear()
                TextBox6.Clear()
                disableButtons()
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'Generate a section
        TextBox4.Text = generateSection(studentNum)

        'Checking if the generateSection() failed
        If TextBox4.Text = "Error" Then
            MsgBox("There's something wrong!")
        Else
            TextBox4.ReadOnly() = False
            Button3.Enabled = True
        End If
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        'Getting the data that the user clicked
        Dim row As DataGridViewRow = DataGridView1.CurrentRow
        Try
            'Displaying the name and email of the user to the TextBoxes
            TextBox1.Text = $"{row.Cells(2).Value.ToString()}, {row.Cells(1).Value.ToString()} {row.Cells(3).Value.ToString()}"
            TextBox6.Text = row.Cells(5).Value.ToString()

            'Clearing the section
            TextBox4.Clear()

            'Setting the Global variables to the value of the students info
            studentNum = row.Cells(0).Value.ToString()
            password = row.Cells(10).Value.ToString()
            fname = row.Cells(1).Value.ToString()
            lname = row.Cells(2).Value.ToString()
            MI = row.Cells(3).Value.ToString()
            year = row.Cells(8).Value.ToString()

            Button1.Enabled() = True
            TextBox4.ReadOnly() = False
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged
        'Enabling REGISTER and ENROLL button if the section in section texbox exist in the database
        If TextBox4.Text.Length() = 2 Then
            Dim sections As String() = getSections(year)
            If sections.Contains(TextBox4.Text.ToUpper()) Then
                Button1.Enabled = True
                Button3.Enabled = True
            End If
            'If it has no text, enabling the GET SECTION button
        ElseIf TextBox4.Text.Length() = 0 Then
            Button1.Enabled = True
        Else
            disableButtons()
        End If
    End Sub

    'Relaoding the Content of the Form when clicked "REFRESH"
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        loadData()
        disableButtons()
        TextBox4.ReadOnly() = True
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        'Enrolling a single student
        enroll(TextBox4.Text.ToUpper(), studentNum, fname, lname, MI, TextBox6.Text, semester, password, AddressOf loadData, AddressOf disableButtons)

        ''Getting the section from TextBox4
        'Dim section As String = TextBox4.Text.ToUpper()

        ''Creating DB-related variables
        'Dim cmd As MySqlCommand
        'Dim cmd2 As MySqlCommand

        'Dim adapter As MySqlDataAdapter
        'Dim ds As New DataSet


        'Try
        '    'Getting the isfull and studentcount
        '    adapter = New MySqlDataAdapter($"select isfull, studentcount from classes where section='{section}'", con)
        '    adapter.Fill(ds)

        '    'Converting those variables into usable ones
        '    Dim isFull As Boolean = Boolean.Parse(ds.Tables(0).Rows(0)(0).ToString())
        '    Dim count As Integer = Integer.Parse(ds.Tables(0).Rows(0)(1).ToString())

        '    'Making changes to the variables
        '    count = count + 1
        '    If count >= 40 Then
        '        isFull = True
        '    End If

        '    'Commiting the changes made to the database
        '    con.Open()
        '    cmd = con.CreateCommand()
        '    cmd.CommandText = "update classes set studentcount=@studentcount, isfull=@isfull where section=@section"
        '    cmd.Parameters.AddWithValue("@studentcount", count)
        '    cmd.Parameters.AddWithValue("@isfull", isFull)
        '    cmd.Parameters.AddWithValue("@section", section)
        '    cmd.ExecuteNonQuery()
        '    con.Close()

        '    'Updating the section of the student as well as sending him his credentials on his email
        '    con.Open()
        '    cmd2 = con.CreateCommand()
        '    cmd2.CommandText = $"update students set section=@section where studentnum='{studentNum}'"
        '    cmd2.Parameters.AddWithValue("@section", section)
        '    cmd2.ExecuteNonQuery()
        '    con.Close()
        '    generateOVRF(fname, lname, MI, studentNum, TextBox6.Text, section)
        '    Dim mail = "Congratulations " & TextBox1.Text & "!!!" & Environment.NewLine & "You are now enrolled at Colegio de Montalban" & Environment.NewLine & "Here are your copy of OVRF and credentials to login in the Colegio de Montalban Application." & Environment.NewLine & "Student Number: " & studentNum & Environment.NewLine & "Password: " & password
        '    sendEmail(mail, "mjbrcns51@gmail.com", "tkzoblulgmuleflh", TextBox6.Text, $"D:\School\3rd Year - Second Semester [3I]\Human Computer Interaction\Project\Programs\Registration\Registration\OVRFs\{studentNum} {lname}, {fname} {MI}.pdf")

        '    'Refreshing the state of the form
        '    loadData()
        '    disableButtons()
        'Catch ex As Exception
        '    MsgBox(ex.ToString)
        'End Try
        'con.Close()

    End Sub

    'Making the form draggable 
    <DllImport("user32.DLL", EntryPoint:="ReleaseCapture")>
    Private Shared Sub ReleaseCapture()
    End Sub
    <DllImport("user32.DLL", EntryPoint:="SendMessage")>
    Private Shared Sub SendMessage(ByVal hWnd As System.IntPtr, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer)
    End Sub

    Private Sub Form1_MouseDown(sender As Object, e As MouseEventArgs) Handles MyBase.MouseDown, Panel1.MouseDown, Label2.MouseDown
        ReleaseCapture()
        SendMessage(Me.Handle, &H112&, &HF012&, 0)
    End Sub

    'Exiting the form since there are no border now
    Private Sub IconButton3_Click(sender As Object, e As EventArgs) Handles IconButton3.Click
        System.Windows.Forms.Application.Exit()
    End Sub

    'Minimizing the form since there are no border now
    Private Sub IconButton1_Click(sender As Object, e As EventArgs) Handles IconButton1.Click
        WindowState = FormWindowState.Minimized
    End Sub

    'Changing the color of the buttons to appropriate ones when hovered
    Private Sub IconButton3_MouseHover(sender As Object, e As EventArgs) Handles IconButton3.MouseHover, IconButton1.MouseHover
        If sender Is IconButton1 Then
            sender.IconColor = Color.Gray
        Else
            sender.IconColor = Color.Red
        End If

    End Sub

    'Reseting the color to white when the mouse leave
    Private Sub IconButton3_MouseLeave(sender As Object, e As EventArgs) Handles IconButton3.MouseLeave, IconButton1.MouseLeave
        sender.IconColor = Color.White
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click, Button6.Click
        'Confirmation if the Admin really wants to Enroll all of the students
        Dim confirm As MsgBoxResult = MsgBox($"Are you sure you want to enroll all the registered studetns?",
                                             vbCritical,
                                             "ONE TIME USE ONLY")
        If confirm = MsgBoxResult.Ok Then
            'Creating year variables
            Dim year1, year2, year3, year4
            Try

                'Selecting all students that are checked by the admin
                Dim adapter As MySqlDataAdapter
                Dim ds As New DataSet
                Dim sectionsToCreate As Integer
                adapter = New MySqlDataAdapter($"SELECT * FROM students WHERE checked = 1 AND section=''", con)
                adapter.Fill(ds)

                'Getting all the student count tell the program how many
                'student will it be enrolling
                Dim studentCount As Integer = ds.Tables(0).Rows.Count

                'Grouping all the data needed
                Dim data = ds.Tables(0).AsEnumerable().[Select](Function(x) New With {
                       Key .year = x.Field(Of String)("year"),
                       Key .studentnum = x.Field(Of String)("studentnum"),
                       Key .firstname = x.Field(Of String)("firstname"),
                       Key .lastname = x.Field(Of String)("lastname"),
                       Key .middlename = x.Field(Of String)("middlename"),
                       Key .email = x.Field(Of String)("email"),
                       Key .accpass = x.Field(Of String)("accpass")
                   }).ToList()

                'Generating a list of 'years' of students
                Dim years(data.Count - 1) As String
                For i As Integer = 0 To data.Count - 1
                    years(i) = data(i).year
                Next

                'Generating a count of the students based on their year
                year1 = years.Where(Function(value) value = "1").Count
                year2 = years.Where(Function(value) value = "2").Count
                year3 = years.Where(Function(value) value = "3").Count
                year4 = years.Where(Function(value) value = "4").Count

                'Creating a list that is of the student by year
                'and their corresponding index
                Dim toBeCreated = {year1, year2, year3, year4}
                Dim yearValues = {1, 2, 3, 4}

                'Creating a sections based on the count of the students per year
                For i As Integer = 0 To toBeCreated.Length - 1
                    If toBeCreated(i) <> 0 Then
                        sectionsToCreate = Math.Ceiling(toBeCreated(i) / 40)
                        createDB(yearValues(i), sectionsToCreate)
                    End If
                Next

                'Enrolling all of the students
                For student As Integer = 0 To data.Count - 1
                    'Generating section for the student
                    Dim section As String = generateSection(data(student).studentnum)

                    'Creating variables to be used for student registration
                    Dim studentnum = data(student).studentnum
                    Dim firstname = data(student).firstname
                    Dim lastname = data(student).lastname
                    Dim middlename = data(student).middlename
                    Dim email = data(student).email
                    Dim accpass = data(student).accpass

                    'Enrolling every one of them
                    enroll(section, studentnum, firstname, lastname, middlename, email, semester, accpass, AddressOf loadData, AddressOf disableButtons)
                Next

            Catch ex As Exception
                MsgBox(ex.ToString())
            End Try


        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        'Declare section and DB-related variables
        Dim section As String = TextBox4.Text.ToUpper()

        Dim cmd As MySqlCommand
        Dim cmd2 As MySqlCommand

        Dim adapter As MySqlDataAdapter
        Dim ds As New DataSet


        Try
            'Get the year of the current student
            adapter = New MySqlDataAdapter($"select year from students where studentnum='{studentNum}'", con)
            adapter.Fill(ds)
            Dim year = ds.Tables(0).Rows(0)(0)
            ds.Reset()

            'Select the coloumn value in DB table that we need
            adapter = New MySqlDataAdapter($"select isfull, studentcount from {year}_year where section='{section}'", con)
            adapter.Fill(ds)

            Dim isFull As Boolean = Boolean.Parse(ds.Tables(0).Rows(0)(0).ToString())
            Dim count As Integer = Integer.Parse(ds.Tables(0).Rows(0)(1).ToString())

            'Change isFull value if it is greater than or equal to 40
            count = count + 1
            If count >= 40 Then
                isFull = True
            End If

            'Commiting those changes to the database
            con.Open()
            cmd = con.CreateCommand()
            cmd.CommandText = "update classes set studentcount=@studentcount, isfull=@isfull where section=@section"
            cmd.Parameters.AddWithValue("@studentcount", count)
            cmd.Parameters.AddWithValue("@isfull", isFull)
            cmd.Parameters.AddWithValue("@section", section)
            cmd.ExecuteNonQuery()
            con.Close()

            'Updating the section of the student and registering them
            con.Open()
            cmd2 = con.CreateCommand()
            cmd2.CommandText = $"update students set checked=1 where studentnum='{studentNum}'"
            cmd2.ExecuteNonQuery()
            con.Close()

            'Refreshing the status of form
            loadData()
            disableButtons()

            'Enabling "ENROLL ALL" button if all of the students are checked
            If DataGridView1.Rows.Count = 0 Then
                Button2.Enabled = True
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        con.Close()
    End Sub
End Class
