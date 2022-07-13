Imports System.IO
Module Functions
    Public Function getStudentCount() As Integer
        Dim currCount As Integer

        'Getting the number in studentNum.txt and converting it to integer

        Using Stream As StreamReader = New StreamReader("D:\Programming\Programs\studentNum.txt")
            currCount = CInt(Stream.ReadLine())
        End Using
        Return currCount
    End Function

    Public Sub updateStudentCount(ByVal count As Integer)
        Using Stream As StreamWriter = New StreamWriter("D:\Programming\Programs\studentNum.txt")
            Stream.WriteLine(CStr(count))
        End Using
    End Sub

    'For generating random numbers which will then be used for generating sections and password

    Public Function GetRandom(ByVal Min As Integer, ByVal Max As Integer) As Integer
        Static Generator As System.Random = New System.Random()
        Return Generator.Next(Min, Max)
    End Function

    'Generating password using the getRandom() function

    Public Function getRandomPassword() As String
        Dim alpha As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        Dim num As String = "1234567890"
        Dim password As String = ""

        For i As Integer = 0 To 7
            Dim n As Integer = GetRandom(0, 2)
            If n Mod 2 = 0 Then
                Dim index = GetRandom(0, num.Length())
                password = password & num(index)
            Else
                Dim index = GetRandom(0, alpha.Length())
                password = password & alpha(index)

            End If
        Next
        Return password

    End Function

    'Get the file that the user will upload and changing the color of buttons based on it

    Public Function getFile(ByVal sender As Object, ByVal type As String) As String
        Dim file As New OpenFileDialog
        file.Multiselect = False
        file.RestoreDirectory = True
        file.Filter = type
        If file.ShowDialog() <> Windows.Forms.DialogResult.Cancel Then
            Dim path As String = file.FileName
            file.FileName = ""
            sender.text = "Great!"
            sender.BackColor = Color.DodgerBlue
            Return path
        Else
            sender.text = "Invalid File!"
            sender.BackColor = Color.Red
        End If
    End Function

    'Saving user uploaded files

    Public Sub saveFile(ByVal sourcePath As String, ByVal toPath As String)
        System.IO.File.Copy(sourcePath, toPath)
    End Sub

    'Update the list of enrolled students

    Public Sub updateEnrolled(ByVal studentNum As String)
        My.Computer.FileSystem.WriteAllText(
            "D:\Programming\Programs\enrolled.txt", $"{studentNum}{Environment.NewLine}", True)
    End Sub

    'Check if the student is enrolled, return false if the list is empty.

    Public Function ifEnrolled(ByVal studentNum As String) As Boolean
        Dim students As String = My.Computer.FileSystem.ReadAllText("D:\Programming\Programs\enrolled.txt")
        If Not students.Length = 0 Then
            Dim studentList = Split(students, $"{Environment.NewLine}")
            Return studentList.Contains(studentNum)
        End If
        Return False
    End Function

    Public Sub RoundButton(btn As Button)
        Dim Raduis As New Drawing2D.GraphicsPath

        Raduis.StartFigure()
        'appends an elliptical arc to the current figure
        'left corner top
        Raduis.AddArc(New Rectangle(0, 0, 20, 20), 180, 90)
        'appends a line segment to the current figure
        Raduis.AddLine(10, 0, btn.Width - 20, 0)
        'appends an elliptical arc to the current figure
        'right corner top
        Raduis.AddArc(New Rectangle(btn.Width - 21, 0, 20, 20), -90, 90)
        'appends a line segment to the current figure
        Raduis.AddLine(btn.Width, 20, btn.Width, btn.Height - 10)
        'appends an elliptical arc to the current figure 
        'right corner buttom
        Raduis.AddArc(New Rectangle(btn.Width - 26, btn.Height - 24, 25, 25), 0, 90)
        'appends a line segment to the current figure
        'left corner bottom
        Raduis.AddLine(btn.Width - 10, btn.Width, 20, btn.Height)
        'appends an elliptical arc to the current figure
        Raduis.AddArc(New Rectangle(0, btn.Height - 20, 20, 20), 90, 90)
        'Close the current figure and start a new one.
        Raduis.CloseFigure()
        'set the window associated with the control
        btn.Region = New Region(Raduis)
    End Sub

    Public Sub roundCorners(obj As Form)
        obj.FormBorderStyle = FormBorderStyle.None


        Dim DGP As New Drawing2D.GraphicsPath
        DGP.StartFigure()
        'top left corner
        DGP.AddArc(New Rectangle(0, 0, 10, 10), 180, 90)
        DGP.AddLine(40, 0, obj.Width - 40, 0)

        'top right corner
        DGP.AddArc(New Rectangle(obj.Width - 10, 0, 10, 10), -90, 90)
        DGP.AddLine(obj.Width, 40, obj.Width, obj.Height - 40)

        'buttom right corner
        DGP.AddArc(New Rectangle(obj.Width - 20, obj.Height - 20, 20, 20), 0, 90)
        DGP.AddLine(obj.Width - 40, obj.Height, 40, obj.Height)

        'buttom left corner
        DGP.AddArc(New Rectangle(0, obj.Height - 10, 10, 10), 90, 90)
        DGP.CloseFigure()

        obj.Region = New Region(DGP)


    End Sub
End Module
