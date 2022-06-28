Imports System.IO
Module Functions
    Public Function getStudentCount() As Integer
        Dim currCount As Integer
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

    Public Function GetRandom(ByVal Min As Integer, ByVal Max As Integer) As Integer
        Static Generator As System.Random = New System.Random()
        Return Generator.Next(Min, Max)
    End Function

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

    Public Function getFile(ByVal type As String) As String
        Dim file As New OpenFileDialog
        file.Multiselect = False
        file.RestoreDirectory = True
        file.Filter = type
        If file.ShowDialog() <> Windows.Forms.DialogResult.Cancel Then
            Dim path As String = file.FileName
            file.FileName = ""
            Return path
        End If
    End Function

    Public Sub saveFile(ByVal sourcePath As String, ByVal toPath As String)
        System.IO.File.Copy(sourcePath, toPath)
    End Sub
End Module
