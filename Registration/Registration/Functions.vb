Imports MySql.Data.MySqlClient
Imports System.Net.Mail
Imports System.Runtime.InteropServices
Imports Microsoft.Office.Interop.Word
Module Functions

    Public str As String = "server=localhost; uid=root; pwd=; database=onlineenrollment"
    Public con As New MySqlConnection(str)
    Public Function GetRandom(ByVal Min As Integer, ByVal Max As Integer) As Integer
        Static Generator As System.Random = New System.Random()
        Return Generator.Next(Min, Max)
    End Function

    Public Function getSections() As String()
        Dim adapter As MySqlDataAdapter
        Dim ds As New DataSet
        con.Open()
        adapter = New MySqlDataAdapter("select * from classes", con)
        con.Close()
        adapter.Fill(ds)
        Dim arr As String() = (From myRow In ds.Tables(0).AsEnumerable
                               Select myRow.Field(Of String)("section")).ToArray

        Return arr
    End Function

    Sub sendEmail(ByVal email As String, ByVal sender As String, ByVal pass As String, ByVal receiver As String, ByVal attachment As String)
        Try
            Dim server As New SmtpClient
            Dim mail As New System.Net.Mail.MailMessage()
            server.UseDefaultCredentials = False
            server.Credentials = New Net.NetworkCredential(sender, pass)
            server.Port = 587
            server.EnableSsl = True
            server.Host = "smtp.gmail.com"

            mail = New System.Net.Mail.MailMessage()
            mail.From = New MailAddress(sender)
            mail.To.Add(receiver)
            mail.Subject = "COLEGIO DE MONTALBAN ENROLLMENT"
            mail.IsBodyHtml = False
            mail.Body = email
            mail.Attachments.Add(New Net.Mail.Attachment(attachment))
            server.Send(mail)
            MsgBox("Mail sent!")

        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub

    Public Sub generateOVRF(ByVal fName As String, ByVal lName As String, ByVal MI As String, ByVal sNum As String, ByVal email As String)
        Dim param As String() = {fName, lName, MI, sNum, email}
        Dim toChange As String() = {"<FNAME>", "<LNAME>", "<MI>", "<SNUM>", "<EMAIL>"}
        Dim wordApp As Application = Nothing
        Try
            wordApp = New Application
            wordApp.Documents.Open("D:\School\3rd Year - Second Semester [3I]\Human Computer Interaction\Project\Programs\Registration\Registration\pig.docx")
            For i As Integer = 0 To 4
                With wordApp.Selection.Find
                    .ClearFormatting()
                    .Text = toChange(i)
                    .Replacement.ClearFormatting()
                    .Replacement.Text = param(i)
                    .Execute(Replace:=2, Forward:=True,
                    Wrap:=1)
                End With
            Next

            wordApp.ActiveDocument.SaveAs2($"D:\School\3rd Year - Second Semester [3I]\Human Computer Interaction\Project\Programs\Registration\Registration\OVRFs\{sNum} {lName}, {fName} {MI}.pdf", WdSaveFormat.wdFormatPDF)
            wordApp.Documents.Close(WdSaveOptions.wdDoNotSaveChanges)
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            If Not IsNothing(wordApp) Then
                wordApp.Quit()
                wordApp = Nothing
            End If
        End Try

    End Sub
End Module
