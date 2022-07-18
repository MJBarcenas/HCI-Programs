Imports MySql.Data.MySqlClient
Imports System.Net.Mail
Imports System.Runtime.InteropServices
Imports Microsoft.Office.Interop.Word
Module Functions
    'Declaring a connection-to-database related variables
    Public str As String = "server=localhost; uid=root; pwd=; database=onlineenrollment"
    Public con As New MySqlConnection(str)

    'A random number generator
    Public Function GetRandom(ByVal Min As Integer, ByVal Max As Integer) As Integer
        Static Generator As System.Random = New System.Random()
        Return Generator.Next(Min, Max)
    End Function

    'Get the available section based on the 
    'current year of thestudent 
    Public Function getSections(ByVal year As String) As String()
        Dim adapter As MySqlDataAdapter
        Dim ds As New DataSet
        con.Open()
        adapter = New MySqlDataAdapter($"select * from {year}_year", con)
        con.Close()
        adapter.Fill(ds)
        Dim arr As String() = (From myRow In ds.Tables(0).AsEnumerable
                               Select myRow.Field(Of String)("section")).ToArray
        Return arr
    End Function

    'Sending an email to the student
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

    'Generate an OVRF for the student
    Public Sub generateOVRF(ByVal fName As String, ByVal lName As String, ByVal MI As String, ByVal sNum As String, ByVal email As String, ByVal section As String, ByVal year As String, ByVal course As String, ByVal semester As String)
        'Dictionary for year of the student to be used in OVRF
        Dim yearDic As String() = {"1st", "2nd", "3rd", "4th"}

        'Creating an Academic Year text for the OVRF
        Dim dateYear = Date.Now.Year
        Dim AY As String
        If semester = "1" Then
            AY = $"{dateYear}-{dateYear + 1}"
        Else
            AY = $"{dateYear - 1}-{dateYear}"
        End If

        'Declaring the parameters that will be replace the 'toChange' list
        Dim param As String() = {fName, lName, MI, sNum, email, section, course, AY, yearDic(CInt(year) - 1)}

        'Declaring the text that the program should search within
        'the active word document
        Dim toChange As String() = {"<FNAME>", "<LNAME>", "<MI>", "<SNUM>", "<EMAIL>", "<S>", "<COURSE>", "<AY>", "<Y>"}

        'Declaring word document which is blank
        Dim wordApp As Application = Nothing
        Try
            'Opening a word document based on the year of the student and current semester
            wordApp = New Application
            MsgBox($"D:\Programming\Programs\Registration\Registration\Word Documents [OVRFs]\{year}_year\{course}\{semester}_sem.docx")
            wordApp.Documents.Open($"D:\Programming\Programs\Registration\Registration\Word Documents [OVRFs]\{year}_year\{course}\{semester}_sem.docx")

            'Changing the word document's content based on the student info
            For i As Integer = 0 To 8
                With wordApp.Selection.Find
                    .ClearFormatting()
                    .Text = toChange(i)
                    .Replacement.ClearFormatting()
                    .Replacement.Text = param(i)
                    .Execute(Replace:=2, Forward:=True,
                    Wrap:=1)
                End With
            Next

            'Saving a copy of the document but not saving the document itself
            'so that it can be used for the next student
            wordApp.ActiveDocument.SaveAs2($"D:\Programming\Programs\Registration\Registration\OVRFs\{year}_year\{course}\{sNum} {lName}, {fName} {MI}.pdf", WdSaveFormat.wdFormatPDF)
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

    Public Sub createDB(ByVal year As Integer, ByVal sections As Integer)
        'Declaring possible sections based on the number of students
        Dim possibleSections = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        Try
            'Creating tables of sections in the database
            'based on the sections parameter
            For i As Integer = 0 To sections - 1
                con.Open()
                Dim cmd As MySqlCommand
                cmd = con.CreateCommand()
                cmd.CommandText = $"INSERT INTO {year}_year(section, studentcount, isfull, year) VALUES('{year}{possibleSections(i)}', 0, 0, '{year}')"
                cmd.ExecuteNonQuery()
                con.Close()
            Next
        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally
            con.Close()
        End Try
    End Sub

    Public Function generateSection(ByVal studentNum As String) As String
        'Declaring the DB-related variables
        Dim adapter As MySqlDataAdapter
        Dim ds As New DataSet

        Try
            'Get the year of the current student
            adapter = New MySqlDataAdapter($"select year from students where studentnum='{studentNum}'", con)
            adapter.Fill(ds)
            Dim year = ds.Tables(0).Rows(0)(0)
            ds.Reset()

            'Get available section for the student
            adapter = New MySqlDataAdapter($"select * from {year}_year where isfull=0 and year='{year}'", con)
            adapter.Fill(ds)

            'If all sections are full, get all of the section
            If ds.Tables(0).Rows.Count = 0 Then
                ds.Clear()
                adapter = New MySqlDataAdapter($"select * from {year}_year where year={year}", con)
                adapter.Fill(ds)
            End If

            'Generate a list of available classes from the dataset
            Dim arr As String() = (From myRow In ds.Tables(0).AsEnumerable
                                   Select myRow.Field(Of String)("section")).ToArray

            'Generate a random section
            Dim len As Integer = arr.Length()
            Dim randomSection As Integer = GetRandom(0, len)

            Return arr(randomSection)
        Catch ex As Exception
            Return "Error"
        End Try
    End Function

    Public Sub enroll(ByVal section As String, ByVal studentNum As String, ByVal fname As String, ByVal lname As String, ByVal MI As String, ByVal email As String, ByVal semester As String, password As String, load As Action, disable As Action)
        'Creating DB-related variables
        Dim cmd As MySqlCommand
        Dim cmd2 As MySqlCommand

        Dim adapter As MySqlDataAdapter
        Dim ds As New DataSet


        Try
            'Getting the isfull and studentcount
            adapter = New MySqlDataAdapter($"select isfull, studentcount from {section(0)}_year where section='{section}'", con)
            adapter.Fill(ds)

            'Converting those variables into usable ones
            Dim isFull As Boolean = Boolean.Parse(ds.Tables(0).Rows(0)(0).ToString())
            Dim count As Integer = Integer.Parse(ds.Tables(0).Rows(0)(1).ToString())

            'Making changes to the variables
            count = count + 1
            If count >= 40 Then
                isFull = True
            End If

            'Commiting the changes made to the database
            con.Open()
            cmd = con.CreateCommand()
            cmd.CommandText = "update classes set studentcount=@studentcount, isfull=@isfull where section=@section"
            cmd.Parameters.AddWithValue("@studentcount", count)
            cmd.Parameters.AddWithValue("@isfull", isFull)
            cmd.Parameters.AddWithValue("@section", section)
            cmd.ExecuteNonQuery()
            con.Close()

            'Updating the section of the student as well as sending him his credentials on his email
            con.Open()
            cmd2 = con.CreateCommand()
            cmd2.CommandText = $"update students set section=@section where studentnum='{studentNum}'"
            cmd2.Parameters.AddWithValue("@section", section)
            cmd2.ExecuteNonQuery()
            con.Close()

            'Getting some info of the student needed for OVRF generation
            ds.Reset()
            adapter = New MySqlDataAdapter($"SELECT year, course FROM students WHERE studentnum='{studentNum}'", con)
            adapter.Fill(ds)

            Dim year As String = ds.Tables(0).Rows(0)(0).ToString
            Dim course As String = ds.Tables(0).Rows(0)(1).ToString

            generateOVRF(fname, lname, MI, studentNum, email, section, year, course, semester)
            Dim mail = $"Congratulations {lname}, {fname}{MI}!!!" & Environment.NewLine & "You are now enrolled at Colegio de Montalban" & Environment.NewLine & "Here are your copy of OVRF and credentials to login in the Colegio de Montalban Application." & Environment.NewLine & "Student Number: " & studentNum & Environment.NewLine & "Password: " & password
            sendEmail(mail, "mjbrcns51@gmail.com", "tkzoblulgmuleflh", email, $"D:\Programming\Programs\Registration\Registration\OVRFs\{year}_year\{course}\{studentNum} {lname}, {fname} {MI}.pdf")

            'Refreshing the state of the form
            load()
            disable()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        con.Close()
    End Sub
End Module
