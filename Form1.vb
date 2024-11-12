Imports System.Net.Http
Imports Newtonsoft.Json.Linq

Public Class Form1
    Private WithEvents Button1 As New Button()
    Private TextBox1 As New TextBox()
    Private NameLabel As New Label()
    Private BirthYearLabel As New Label()

    Private PADDING_SIZE As Integer = 10

    Private Function FullWidth()
        Return Me.ClientRectangle.Width - Me.PADDING_SIZE * 2
    End Function
    Private Function FullHeight()
        Return Me.ClientRectangle.Height - Me.PADDING_SIZE * 2
    End Function

    Public Sub New()
        ' Initialize the form
        Me.InitializeComponent()

        ' The display area for the API response
        TextBox1.Multiline = True
        TextBox1.ScrollBars = ScrollBars.None
        TextBox1.Top = Me.PADDING_SIZE
        TextBox1.Left = Me.PADDING_SIZE
        TextBox1.Width = Me.FullWidth()
        TextBox1.Height = Me.FullHeight() - Button1.Height - NameLabel.Height - BirthYearLabel.Height - Me.PADDING_SIZE * 3
        Me.Controls.Add(TextBox1)

        ' Labels for extracted values
        NameLabel.Text = "Name:"
        NameLabel.Top = TextBox1.Bottom + Me.PADDING_SIZE
        NameLabel.Left = Me.PADDING_SIZE
        BirthYearLabel.Text = "Birth Year:"
        BirthYearLabel.Top = NameLabel.Bottom + Me.PADDING_SIZE
        BirthYearLabel.Left = Me.PADDING_SIZE
        Me.Controls.Add(NameLabel)
        Me.Controls.Add(BirthYearLabel)

        ' The Button to send an API request
        Button1.Text = "Fetch Data"
        Button1.Top = BirthYearLabel.Bottom + Me.PADDING_SIZE
        Button1.Left = Me.PADDING_SIZE
        Button1.Width = Me.FullWidth()
        Me.Controls.Add(Button1)
    End Sub

    Private Async Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Using client As New HttpClient()
            Try
                ' Get a JSON response
                Dim response As HttpResponseMessage = Await client.GetAsync("https://swapi.dev/api/people/1")
                response.EnsureSuccessStatusCode()
                Dim responseBody As String = Await response.Content.ReadAsStringAsync()

                ' Parse the response body as JSON
                Dim json As JObject = JObject.Parse(responseBody)
                Dim name As String = json("name").ToString()
                Dim birthYear As String = json("birth_year").ToString()

                ' Display the extracted values
                TextBox1.Text = responseBody
                NameLabel.Text = $"Name: {name}"
                BirthYearLabel.Text = $"Birth Year: {birthYear}"

            Catch ex As Exception
                TextBox1.Text = "Error: " & ex.Message
            End Try
        End Using
    End Sub
End Class
