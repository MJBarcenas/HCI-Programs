Imports System.IO
Module Functions
    Public Function getStudentCount() As Integer
        Dim currCount As Integer
        Using Stream As StreamReader = New StreamReader("D:\School\3rd Year - Second Semester [3I]\Human Computer Interaction\Project\Programs\onSiteEnrollment\onSiteEnrollment\studentNum.txt")
            currCount = CInt(Stream.ReadLine())
        End Using
        Return currCount
    End Function

    Public Sub updateStudentCount(ByVal count As Integer)
        Using Stream As StreamWriter = New StreamWriter("D:\School\3rd Year - Second Semester [3I]\Human Computer Interaction\Project\Programs\onSiteEnrollment\onSiteEnrollment\studentNum.txt")
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

        For i As Integer = 0 To 8
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
End Module
