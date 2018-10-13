Imports System.Environment
Imports System.IO

Public Class Form1

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        OpenFileDialog1.FileName = "gridcoinresearch.conf"
        OpenFileDialog1.Filter = "Gridcoin Research Conf File (gridcoinresearch.conf)|gridcoinresearch.conf|Other Conf File (*.conf)|*.conf|All Files(*.*)|*.*"
        OpenFileDialog1.Title = "Search for the Gridcoin Research Conf File"
        OpenFileDialog1.ShowDialog()
        TextBox1.Text = OpenFileDialog1.FileName
        loadData(OpenFileDialog1.FileName)
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim GridcoinConfigFile As String = GetFolderPath(SpecialFolder.ApplicationData) & "\GridcoinResearch\gridcoinresearch.conf"
        If My.Computer.FileSystem.FileExists(GridcoinConfigFile) Then
            TextBox1.Text = GridcoinConfigFile
            loadData(GridcoinConfigFile)
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim FieldsEmpty As Boolean = False
        Dim RPCIPEmpty As Boolean = False
        Dim RPCPortEmpty As Boolean = False
        Dim RPCUsernameEmpty As Boolean = False
        Dim RPCPasswordEmpty As Boolean = False
        Dim PortIsNotNumeric As Boolean = False
        Dim ErrorFound As Boolean = False
        Dim EmptyFieldMessage As String = "The following fields are empty: " & vbNewLine
        If String.IsNullOrEmpty(RichTextBox1.Text) Then
            EmptyFieldMessage += "-IP Addresses" & vbNewLine
            RPCIPEmpty = True
            FieldsEmpty = True
            ErrorFound = True
        End If
        If String.IsNullOrEmpty(PortText.Text) Or PortText.Text.Contains(" ") Then
            EmptyFieldMessage += "-Port Number" & vbNewLine
            RPCPortEmpty = True
            FieldsEmpty = True
            ErrorFound = True
        End If
        If String.IsNullOrEmpty(UsernameField.Text) Or UsernameField.Text.Contains(" ") Then
            EmptyFieldMessage += "-Username" & vbNewLine
            RPCUsernameEmpty = True
            FieldsEmpty = True
            ErrorFound = True
        End If
        If String.IsNullOrEmpty(PasswordField.Text) Or PasswordField.Text.Contains(" ") Then
            EmptyFieldMessage += "-Password" & vbNewLine
            RPCPasswordEmpty = True
            FieldsEmpty = True
            ErrorFound = True
        End If
        If IsNumeric(PortText.Text) = False And RPCPortEmpty = False Then
            PortIsNotNumeric = True
            ErrorFound = True
        Else
            PortIsNotNumeric = False
        End If
        If ErrorFound = True Then
            If FieldsEmpty = True Then
                EmptyFieldMessage += "Please enter the correct information in order to save the config file"
                MessageBox.Show(EmptyFieldMessage)
            End If
            If PortIsNotNumeric = True Then
                MessageBox.Show("The entered Port Number is not a number. Please enter a number in order to save the config file")
            End If
        Else
            Dim line As String
            Dim ServerEnabled As String = "0"
            Dim ServerLineFound As Boolean = False
            Dim RPCIPFound As Boolean = False
            Dim RPCPortFound As Boolean = False
            Dim RPCUsernameFound As Boolean = False
            Dim RPCPasswordFound As Boolean = False
            Dim wroteIPAddresses As Boolean = False
            If CheckBox1.Checked Then ServerEnabled = "1" Else ServerEnabled = "0"
            My.Computer.FileSystem.CopyFile(TextBox1.Text, TextBox1.Text & ".rpcbak", True)
            Dim writer As New StreamWriter(TextBox1.Text & ".temp")
            Using reader As New StreamReader(TextBox1.Text)
                While Not reader.EndOfStream
                    line = reader.ReadLine()
                    If line.Contains("server=") Then
                        ServerLineFound = True
                        writer.WriteLine("server=" & ServerEnabled)
                    ElseIf line.Contains("rpcallowip") Then
                        If wroteIPAddresses = False Then
                            RPCIPFound = True
                            For i As Integer = 0 To RichTextBox1.Lines.Length - 1
                                If String.IsNullOrEmpty(RichTextBox1.Lines(i)) = False Then If RichTextBox1.Lines(i).Contains(" ") = False Then writer.WriteLine("rpcallowip=" & RichTextBox1.Lines(i))
                            Next
                            wroteIPAddresses = True
                        End If
                    ElseIf line.Contains("rpcport") Then
                        RPCPortFound = True
                        writer.WriteLine("rpcport=" & PortText.Text)
                    ElseIf line.Contains("rpcuser") Then
                        RPCUsernameFound = True
                        writer.WriteLine("rpcuser=" & UsernameField.Text)
                    ElseIf line.Contains("rpcpassword") Then
                        RPCPasswordFound = True
                        writer.WriteLine("rpcpassword=" & PasswordField.Text)
                    Else
                        writer.WriteLine(line)
                    End If
                End While
            End Using
            If ServerLineFound = False Then writer.WriteLine("server=" & ServerEnabled)
            If RPCIPFound = False Then
                For i As Integer = 0 To RichTextBox1.Lines.Length - 1
                    writer.WriteLine("rpcallowip=" & RichTextBox1.Lines(i))
                Next
            End If
            If RPCPortFound = False Then writer.WriteLine("rpcport=" & PortText.Text)
            If RPCUsernameFound = False Then writer.WriteLine("rpcuser=" & UsernameField.Text)
            If RPCPasswordFound = False Then writer.WriteLine("rpcpassword=" & PasswordField.Text)
            writer.Close()
            My.Computer.FileSystem.MoveFile(TextBox1.Text & ".temp", TextBox1.Text, True)
            MessageBox.Show("Config File saved successfully! Please restart the Gridcoin Research Wallet for the changes to take effect")
        End If

    End Sub
    Private Sub loadData(ConfigFile As String)
        Dim line As String
        Dim getdata As String()
        Dim result As String
        RichTextBox1.Clear()
        Using reader As New StreamReader(ConfigFile)
            While Not reader.EndOfStream
                line = reader.ReadLine()
                If line.Contains("server=") Then
                    getdata = line.Split("=")
                    result = getdata(1)
                    If result = "1" Then CheckBox1.Checked = True Else CheckBox1.Checked = False
                ElseIf line.Contains("rpcallowip") Then
                    getdata = line.Split("=")
                    result = getdata(1)
                    RichTextBox1.Text += result & vbNewLine
                ElseIf line.Contains("rpcport") Then
                    getdata = line.Split("=")
                    result = getdata(1)
                    PortText.Text = result
                ElseIf line.Contains("rpcuser") Then
                    getdata = line.Split("=")
                    result = getdata(1)
                    UsernameField.Text = result
                ElseIf line.Contains("rpcpassword") Then
                    getdata = line.Split("=")
                    result = getdata(1)
                    PasswordField.Text = result
                End If
            End While
        End Using
    End Sub
End Class
