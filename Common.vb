Imports System.IO
Imports System.Security.Cryptography
Imports System.Text
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Reflection
Imports System.Text.RegularExpressions
Imports Microsoft.Office.Interop
Imports System.Net.Dns


Namespace GSRS.Net.Common.Scripts
    ''' <summary>
    ''' Simple encryption decryption of strings
    ''' </summary>
    Public Class HelperClass
        Private Const secretKey As String = "GSRSVA"

        Public Function Encrypt(plainText As String) As String
            Dim encryptedPassword As String
            Using outputStream = New MemoryStream()
                Dim algorithm As RijndaelManaged = getAlgorithm()
                Using cryptoStream =
                    New CryptoStream(outputStream, algorithm.CreateEncryptor(),
                                     CryptoStreamMode.Write)
                    Dim inputBuffer() As Byte = Encoding.Unicode.GetBytes(plainText)
                    cryptoStream.Write(inputBuffer, 0, inputBuffer.Length)
                    cryptoStream.FlushFinalBlock()
                    encryptedPassword = Convert.ToBase64String(outputStream.ToArray())
                End Using
            End Using
            Return encryptedPassword
        End Function

        Public Function Decrypt(encryptedText As String) As String
            Dim encryptedBytes As Byte()
            encryptedBytes = Convert.FromBase64String(encryptedText)
            Dim plainText As String = Nothing
            Using inputStream = New MemoryStream(encryptedBytes)
                Dim algorithm As RijndaelManaged = getAlgorithm()
                Using cryptoStream =
                    New CryptoStream(inputStream, algorithm.CreateDecryptor(),
                                     CryptoStreamMode.Read)
                    Dim outputBuffer(0 To CType(inputStream.Length - 1, Integer)) As Byte
                    Dim readBytes As Integer =
                            cryptoStream.Read(outputBuffer, 0, CType(inputStream.Length, Integer))
                    plainText = Encoding.Unicode.GetString(outputBuffer, 0, readBytes)
                End Using
            End Using
            Return plainText
        End Function

        Private Function getAlgorithm() As RijndaelManaged
            Const salt As String = "va~jdf"
            Const keySize As Integer = 256

            Dim keyBuilder = New Rfc2898DeriveBytes(secretKey, Encoding.Unicode.GetBytes(salt))
            Dim algorithm = New RijndaelManaged()
            algorithm.KeySize = keySize
            algorithm.IV = keyBuilder.GetBytes(CType(algorithm.BlockSize / 8, Integer))
            algorithm.Key = keyBuilder.GetBytes(CType(algorithm.KeySize / 8, Integer))
            algorithm.Padding = PaddingMode.PKCS7
            Return algorithm
        End Function


        Public Function VerifyUser(UserName As String, Password As String, ByRef UserProfile As Dictionary(Of String, String), ByRef StatusText As String) As Boolean
            Dim VerifyResult As Boolean = False
            Dim SQLConnString As String = GetConnStringFromConfig()

            Dim SQLDReader As SqlDataReader
            Dim UserStatus As String
            'SQLConnString = "Data Source=DESKTOP-5KKQDD5\SQLEXPRESS;Initial Catalog=GSRS;User Id=vampadu;Password=test123;Integrated Security=False"
            Using SQLConn As New SqlConnection With {.ConnectionString = SQLConnString}
                Try
                    SQLConn.Open()

                    Dim SQLString As String = "Select FName, LName, Status from [User_Profile] Where [UserName] ='" & UserName & "' AND [Password] = '" & Encrypt(Password) & "'"

                    'Define the SqlCommand object
                    Dim SQLCmd As SqlCommand = New SqlCommand(SQLString, SQLConn)
                    'set the SqlCommand type to stored procedure And execute
                    SQLCmd.CommandType = CommandType.Text
                    SQLDReader = SQLCmd.ExecuteReader()

                    If SQLDReader.HasRows Then
                        While SQLDReader.Read()
                            UserProfile.Add("FName", SQLDReader("FName").ToString)
                            UserProfile.Add("LName", SQLDReader("LName").ToString)
                            UserStatus = SQLDReader("Status").ToString
                            UserStatus = UserStatus.Replace(vbCrLf, "").Trim.ToUpper

                            UserProfile.Add("Status", UserStatus)
                            VerifyResult = True
                        End While
                    End If

                Catch failedLoginException As SqlException When failedLoginException.Number = 18456
                    StatusText = failedLoginException.Message

                Catch genericSqlException As SqlException
                    'loginResult.Success = False
                    'loginResult.GenericException = False
                    'loginResult.Message = "Can not access data."
                    StatusText = genericSqlException.Message
                Catch ex As Exception
                    StatusText = ex.Message
                End Try
            End Using


            Return VerifyResult
        End Function
        Public Function VerifyUserByName(UserName As String, ByRef StatusText As String) As Boolean
            Dim VerifyResult As Boolean = False
            Dim SQLConnString As String = GetConnStringFromConfig()

            Dim SQLDReader As SqlDataReader
            Using SQLConn As New SqlConnection With {.ConnectionString = SQLConnString}
                Try
                    SQLConn.Open()

                    Dim SQLString As String = "Select * from [User_Profile] Where [UserName] ='" & UserName & "'"

                    'Define the SqlCommand object
                    Dim SQLCmd As SqlCommand = New SqlCommand(SQLString, SQLConn)
                    'set the SqlCommand type to stored procedure And execute
                    SQLCmd.CommandType = CommandType.Text
                    SQLDReader = SQLCmd.ExecuteReader()

                    If SQLDReader.HasRows Then
                        While SQLDReader.Read()
                            VerifyResult = True
                        End While

                    End If

                Catch failedLoginException As SqlException When failedLoginException.Number = 18456
                    'loginResult.Success = False
                    'loginResult.GenericException = False
                    'loginResult.Message = "Can not access data."
                Catch genericSqlException As SqlException
                    'loginResult.Success = False
                    'loginResult.GenericException = False
                    'loginResult.Message = "Can not access data."
                Catch ex As Exception
                    'loginResult.Success = False
                    'loginResult.GenericException = True
                    'loginResult.Message = ex.Message
                End Try
            End Using


            Return VerifyResult
        End Function

        Public Function GetConnStringFromConfig() As String
            Dim SQLConnString As String = ""

            Try
                'Dim myDLLConfig As Configuration = ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location)
                'Dim appSettingsConfig As AppSettingsSection = CType(myDLLConfig.GetSection("appSettings"), AppSettingsSection)

                'SQLConnString = $"Data Source={appSettingsConfig.Settings("DB_Server").Value};" &
                '    $"Initial Catalog={appSettingsConfig.Settings("DB_Catalog").Value};" &
                '    $"User Id={Decrypt(appSettingsConfig.Settings("DB_UserName").Value)};Password={Decrypt(appSettingsConfig.Settings("DB_UserPwd").Value)};" &
                '    "Integrated Security=False"

                Dim ComputerName As String = System.Net.Dns.GetHostName

                Dim DBServer As String = ComputerName & "\SQLEXPRESS"

                SQLConnString = $"Data Source=" & DBServer & ";Initial Catalog=GSRS;User Id=admin;Password=password;Integrated Security=False"


                'SQLConnString = $"Data Source={ ConfigurationManager.ConnectionStrings("DB_Server").ConnectionString};" &
                '    $"Initial Catalog={ ConfigurationManager.ConnectionStrings("DB_Catalog").ConnectionString};" &
                '    $"User Id={ConfigurationManager.ConnectionStrings("DB_UserName").ConnectionString};Password={ConfigurationManager.ConnectionStrings("DB_UserPwd").ConnectionString};" &
                '    "Integrated Security=False"



                Return SQLConnString

            Catch ex As Exception
                MessageBox.Show("In Function GetConnStringFromConfig - " & ex.Message)
                SQLConnString = ""
                Return SQLConnString
            End Try
        End Function

        Public Function InsertSlopeData(dtSlopeTable As DataTable, RequestType As String) As Boolean

            Dim InsertData As Boolean = False

            Try
                Dim SQLConnString As String = GetConnStringFromConfig()

                Dim SQLDReader As SqlDataReader
                Dim SPName As String = ""

                Using SQLConn As New SqlConnection With {.ConnectionString = SQLConnString}
                    Try
                        If RequestType = "ContinuousSlope" Then
                            SPName = "[dbo].[InsertCSlopeData]"
                        ElseIf RequestType = "SeparateSlope" Then
                            SPName = "[dbo].[InsertSSlopeData]"
                        End If


                        'Define the SqlCommand object
                        Dim SQLCmd As SqlCommand = New SqlCommand(SPName, SQLConn)

                        ' Configure the command and parameter.
                        Dim STParam As SqlParameter = SQLCmd.Parameters.AddWithValue("@RequestType", RequestType)
                        STParam.SqlDbType = SqlDbType.VarChar

                        STParam = SQLCmd.Parameters.AddWithValue("@SlopeType", dtSlopeTable)
                        STParam.SqlDbType = SqlDbType.Structured
                        If RequestType = "ContinuousSlope" Then
                            STParam.TypeName = "dbo.CSlopeType"
                        ElseIf RequestType = "SeparateSlope" Then
                            STParam.TypeName = "dbo.SSlopeType"
                        End If



                        'set the SqlCommand type to stored procedure And execute
                        SQLCmd.CommandType = CommandType.StoredProcedure
                        SQLConn.Open()

                        SQLDReader = SQLCmd.ExecuteReader()
                        InsertData = True


                    Catch failedLoginException As SqlException When failedLoginException.Number = 18456
                        'loginResult.Success = False
                        MessageBox.Show(failedLoginException.Message)
                    Catch genericSqlException As SqlException
                        MessageBox.Show(genericSqlException.Message)
                    Catch ex As Exception
                        MessageBox.Show(ex.Message)
                    End Try
                End Using

                Return InsertData
            Catch ex As Exception
                MessageBox.Show("In Function InsertSlopeData - " & ex.Message)
                Return InsertData
            End Try
        End Function

        Public Function InsertTempProfileData(dtTempPTable As DataTable) As Boolean

            Dim InsertData As Boolean = False

            Try
                Dim SQLConnString As String = GetConnStringFromConfig()

                Dim SQLDReader As SqlDataReader
                Dim SPName As String = ""

                Using SQLConn As New SqlConnection With {.ConnectionString = SQLConnString}
                    Try
                        SPName = "[dbo].[InsertCTempProfileData]"

                        'Define the SqlCommand object
                        Dim SQLCmd As SqlCommand = New SqlCommand(SPName, SQLConn)

                        ' Configure the command and parameter.
                        Dim STParam As SqlParameter = SQLCmd.Parameters.AddWithValue("@CTProfileType", dtTempPTable)
                        STParam.SqlDbType = SqlDbType.Structured
                        STParam.TypeName = "dbo.CTProfileType"

                        'set the SqlCommand type to stored procedure And execute
                        SQLCmd.CommandType = CommandType.StoredProcedure
                        SQLConn.Open()

                        SQLDReader = SQLCmd.ExecuteReader()
                        InsertData = True


                    Catch failedLoginException As SqlException When failedLoginException.Number = 18456
                        'loginResult.Success = False
                        MessageBox.Show(failedLoginException.Message)
                    Catch genericSqlException As SqlException
                        MessageBox.Show(genericSqlException.Message)
                    Catch ex As Exception
                        MessageBox.Show(ex.Message)
                    End Try
                End Using

                Return InsertData
            Catch ex As Exception
                MessageBox.Show("In Function InsertTempProfileData - " & ex.Message)
                Return InsertData
            End Try
        End Function

        Public Function InsertUserDetails(FName As String, LName As String, UserName As String, UserPwd As String, UserStatus As String) As Boolean

            Dim InsertStatus As Boolean

            Try
                Dim SQLConnString As String = GetConnStringFromConfig()

                Dim SQLDReader As SqlDataReader
                Dim SQLString As String = ""

                Using SQLConn As New SqlConnection With {.ConnectionString = SQLConnString}
                    Try
                        SQLString = "INSERT INTO [dbo].[User_Profile] ([FName],[LName],[UserName],[Password],[Status]) VALUES ("
                        SQLString = SQLString & "'" & FName & "',"
                        SQLString = SQLString & "'" & LName & "',"
                        SQLString = SQLString & "'" & UserName & "',"
                        SQLString = SQLString & "'" & Encrypt(UserPwd) & "',"
                        SQLString = SQLString & "'" & UserStatus & "')"

                        'Define the SqlCommand object
                        Dim SQLCmd As SqlCommand = New SqlCommand(SQLString, SQLConn)


                        'set the SqlCommand type to stored procedure And execute
                        SQLCmd.CommandType = CommandType.Text
                        SQLConn.Open()

                        SQLDReader = SQLCmd.ExecuteReader()
                        InsertStatus = True

                    Catch failedLoginException As SqlException When failedLoginException.Number = 18456
                        'loginResult.Success = False
                        MessageBox.Show(failedLoginException.Message)
                    Catch genericSqlException As SqlException
                        MessageBox.Show(genericSqlException.Message)
                    Catch ex As Exception
                        MessageBox.Show(ex.Message)
                    End Try
                End Using

                Return InsertStatus
            Catch ex As Exception
                MessageBox.Show("In Function InsertUserDetails - " & ex.Message)
                Return InsertStatus
            End Try
        End Function

        Public Function UpdateUserDetails(FName As String, LName As String, UserName As String, UserPwd As String, UserStatus As String) As Boolean

            Dim InsertStatus As Boolean

            Try
                Dim SQLConnString As String = GetConnStringFromConfig()

                Dim SQLDReader As SqlDataReader
                Dim SQLString As String = ""

                Using SQLConn As New SqlConnection With {.ConnectionString = SQLConnString}
                    Try
                        'UPDATE [dbo].[User_Profile] SET [FName] = '',[LName] = '',[Password] = '',[Status] = ''  WHERE UserName = ''
                        SQLString = "UPDATE [dbo].[User_Profile] SET [FName] = '" & FName & "',"
                        SQLString = SQLString & " [LName] = '" & LName & "',"
                        SQLString = SQLString & " [Password] = '" & Encrypt(UserPwd) & "',"
                        SQLString = SQLString & " [Status] = '" & UserStatus & "'"
                        SQLString = SQLString & " WHERE [UserName] = '" & UserName & "'"


                        'Define the SqlCommand object
                        Dim SQLCmd As SqlCommand = New SqlCommand(SQLString, SQLConn)


                        'set the SqlCommand type to stored procedure And execute
                        SQLCmd.CommandType = CommandType.Text
                        SQLConn.Open()

                        SQLDReader = SQLCmd.ExecuteReader()
                        InsertStatus = True

                    Catch failedLoginException As SqlException When failedLoginException.Number = 18456
                        'loginResult.Success = False
                        MessageBox.Show(failedLoginException.Message)
                    Catch genericSqlException As SqlException
                        MessageBox.Show(genericSqlException.Message)
                    Catch ex As Exception
                        MessageBox.Show(ex.Message)
                    End Try
                End Using

                Return InsertStatus
            Catch ex As Exception
                MessageBox.Show("In Function UpdateUserDetails - " & ex.Message)
                Return InsertStatus
            End Try
        End Function

        Public Function DeleteUser(UserName As String) As Boolean

            Dim DeleteStatus As Boolean

            Try
                Dim SQLConnString As String = GetConnStringFromConfig()

                Dim SQLDReader As SqlDataReader
                Dim SQLString As String = ""

                Using SQLConn As New SqlConnection With {.ConnectionString = SQLConnString}
                    Try
                        SQLString = "DELETE FROM [dbo].[User_Profile] WHERE [UserName] = '" & UserName & "'"

                        'Define the SqlCommand object
                        Dim SQLCmd As SqlCommand = New SqlCommand(SQLString, SQLConn)


                        'set the SqlCommand type to stored procedure And execute
                        SQLCmd.CommandType = CommandType.Text
                        SQLConn.Open()

                        SQLDReader = SQLCmd.ExecuteReader()
                        DeleteStatus = True

                    Catch failedLoginException As SqlException When failedLoginException.Number = 18456
                        'loginResult.Success = False
                        MessageBox.Show(failedLoginException.Message)
                    Catch genericSqlException As SqlException
                        MessageBox.Show(genericSqlException.Message)
                    Catch ex As Exception
                        MessageBox.Show(ex.Message)
                    End Try
                End Using

                Return DeleteStatus
            Catch ex As Exception
                MessageBox.Show("In Function DeleteUser - " & ex.Message)
                Return DeleteStatus
            End Try
        End Function

        Public Function CleanUpUserData(LoggedOnUser As String) As Boolean

            Dim CleanUpStatus As Boolean

            Try
                Dim SQLConnString As String = GetConnStringFromConfig()

                Dim SQLDReader As SqlDataReader
                Dim SPName As String = ""

                Using SQLConn As New SqlConnection With {.ConnectionString = SQLConnString}
                    Try
                        SPName = "[dbo].[CleanUpTableData]"

                        'Define the SqlCommand object
                        Dim SQLCmd As SqlCommand = New SqlCommand(SPName, SQLConn)

                        ' Configure the command and parameter.
                        Dim STParam As SqlParameter = SQLCmd.Parameters.AddWithValue("@LoggedOnUser", LoggedOnUser)
                        STParam.SqlDbType = SqlDbType.VarChar

                        'set the SqlCommand type to stored procedure And execute
                        SQLCmd.CommandType = CommandType.StoredProcedure
                        SQLConn.Open()

                        SQLDReader = SQLCmd.ExecuteReader()
                        CleanUpStatus = True


                    Catch failedLoginException As SqlException When failedLoginException.Number = 18456
                        'loginResult.Success = False
                        MessageBox.Show(failedLoginException.Message)
                    Catch genericSqlException As SqlException
                        MessageBox.Show(genericSqlException.Message)
                    Catch ex As Exception
                        MessageBox.Show(ex.Message)
                    End Try
                End Using

                Return CleanUpStatus
            Catch ex As Exception
                MessageBox.Show("In Function CleanUpUserData - " & ex.Message)
                Return CleanUpStatus
            End Try
        End Function

        Public Function GetUserDetails() As DataTable

            Dim UserTable As New DataTable
            Try
                Dim SQLConnString As String = GetConnStringFromConfig()

                Dim SQLDReader As SqlDataReader
                Dim SQLString As String = ""

                Using SQLConn As New SqlConnection With {.ConnectionString = SQLConnString}
                    Try
                        SQLString = "SELECT FName,LName,UserName,Password,Status FROM [dbo].[User_Profile] order by FName"

                        'Define the SqlCommand object
                        Dim SQLCmd As SqlCommand = New SqlCommand(SQLString, SQLConn)


                        'set the SqlCommand type to stored procedure And execute
                        SQLCmd.CommandType = CommandType.Text
                        SQLConn.Open()

                        SQLDReader = SQLCmd.ExecuteReader()
                        UserTable.Load(SQLDReader)


                    Catch failedLoginException As SqlException When failedLoginException.Number = 18456
                        'loginResult.Success = False
                        MessageBox.Show(failedLoginException.Message)
                    Catch genericSqlException As SqlException
                        MessageBox.Show(genericSqlException.Message)
                    Catch ex As Exception
                        MessageBox.Show(ex.Message)
                    End Try
                End Using

                Return UserTable
            Catch ex As Exception
                MessageBox.Show("In Function GetUserDetails - " & ex.Message)
                Return UserTable
            End Try
        End Function

        ' GetSlopeData gets the data from the tables and populates into a datatable which will be used to write to excel file
        Public Function GetSlopeData(RequestType As String, UniqueId As String, XLFileName As String) As Boolean

            Dim UserTable As New DataTable
            Dim FileGenerated As Boolean = False

            Try
                Dim SQLConnString As String = GetConnStringFromConfig()

                Dim SQLDReader As SqlDataReader
                Dim SQLString As String = ""

                Using SQLConn As New SqlConnection With {.ConnectionString = SQLConnString}
                    Try

                        If RequestType = "ContinuousSlope" Then
                            SQLString = "SELECT Max_Weight, Max_Speed, T_Desc, T_Emerg, T_Final, Time FROM [dbo].[ContinuousSlopeOutput] where Unique_Id = '" & UniqueId & "' order by Max_Weight desc"
                        ElseIf RequestType = "SeparateSlope" Then
                            SQLString = "SELECT Number_Grades,Group_Number, Max_Weight, Max_Speed, T_Desc, T_Emerg, T_Final, Time FROM [dbo].[SeparateSlopeOutput] where Unique_Id = '" & UniqueId & "' order by Max_Weight desc"
                        End If


                        'Define the SqlCommand object
                        Dim SQLCmd As SqlCommand = New SqlCommand(SQLString, SQLConn)


                        'set the SqlCommand type to stored procedure And execute
                        SQLCmd.CommandType = CommandType.Text
                        SQLConn.Open()

                        SQLDReader = SQLCmd.ExecuteReader()
                        UserTable.Load(SQLDReader)

                        FileGenerated = WriteToXL(RequestType, XLFileName, UserTable)

                        If FileGenerated Then
                            'Delete the records from the table
                            FileGenerated = DeleteTableData(RequestType, UniqueId)
                        End If


                    Catch failedLoginException As SqlException When failedLoginException.Number = 18456
                        'loginResult.Success = False
                        MessageBox.Show(failedLoginException.Message)
                    Catch genericSqlException As SqlException
                        MessageBox.Show(genericSqlException.Message)
                    Catch ex As Exception
                        MessageBox.Show(ex.Message)
                    End Try
                End Using


                Return FileGenerated
            Catch ex As Exception
                MessageBox.Show("In Function GetSlopeData - " & ex.Message)
                Return FileGenerated
            End Try
        End Function

        Public Function GetTempProfileData(UniqueId As String, XLFileName As String) As Boolean

            Dim UserTable As New DataTable
            Dim FileGenerated As Boolean = False

            Try
                Dim SQLConnString As String = GetConnStringFromConfig()

                Dim SQLDReader As SqlDataReader
                Dim SQLString As String = ""

                Using SQLConn As New SqlConnection With {.ConnectionString = SQLConnString}
                    Try

                        SQLString = "SELECT [T_Weight],[T_Speed],[T_Distance],[T_Grade_Pct],[T_Desc],[T_Emerg],[T_Final] From [GSRS].[dbo].[ContinuousTempProfile] where Unique_Id = '" & UniqueId & "' order by T_Weight desc"

                        'Define the SqlCommand object
                        Dim SQLCmd As SqlCommand = New SqlCommand(SQLString, SQLConn)


                        'set the SqlCommand type to stored procedure And execute
                        SQLCmd.CommandType = CommandType.Text
                        SQLConn.Open()

                        SQLDReader = SQLCmd.ExecuteReader()
                        UserTable.Load(SQLDReader)

                        FileGenerated = WriteToXL("TempProfile", XLFileName, UserTable)

                        If FileGenerated Then
                            'Delete the records from the table
                            FileGenerated = DeleteTableData("TempProfile", UniqueId)
                        End If
                    Catch failedLoginException As SqlException When failedLoginException.Number = 18456
                        'loginResult.Success = False
                        MessageBox.Show(failedLoginException.Message)
                    Catch genericSqlException As SqlException
                        MessageBox.Show(genericSqlException.Message)
                    Catch ex As Exception
                        MessageBox.Show(ex.Message)
                    End Try
                End Using

                Return FileGenerated
            Catch ex As Exception
                MessageBox.Show("In Function GetTempProfileData - " & ex.Message)

                Return FileGenerated
            End Try
        End Function

        Public Function DeleteTableData(RequestType As String, UniqueId As String) As Boolean

            Dim DeleteStatus As Boolean

            Try
                Dim SQLConnString As String = GetConnStringFromConfig()

                Dim SQLDReader As SqlDataReader
                Dim SQLString As String = ""

                Using SQLConn As New SqlConnection With {.ConnectionString = SQLConnString}
                    Try
                        If RequestType = "ContinuousSlope" Then
                            SQLString = "DELETE FROM [dbo].[ContinuousSlopeOutput] WHERE [Unique_Id] = '" & UniqueId & "'"
                        ElseIf RequestType = "SeparateSlope" Then
                            SQLString = "DELETE FROM [dbo].[SeparateSlopeOutput] WHERE [Unique_Id] = '" & UniqueId & "'"
                        ElseIf RequestType = "TempProfile" Then
                            SQLString = "DELETE FROM [dbo].[ContinuousTempProfile] WHERE [Unique_Id] = '" & UniqueId & "'"
                        End If

                        'Define the SqlCommand object
                        Dim SQLCmd As SqlCommand = New SqlCommand(SQLString, SQLConn)


                        'set the SqlCommand type to stored procedure And execute
                        SQLCmd.CommandType = CommandType.Text
                        SQLConn.Open()

                        SQLDReader = SQLCmd.ExecuteReader()
                        DeleteStatus = True

                    Catch failedLoginException As SqlException When failedLoginException.Number = 18456
                        'loginResult.Success = False
                        MessageBox.Show(failedLoginException.Message)
                    Catch genericSqlException As SqlException
                        MessageBox.Show(genericSqlException.Message)
                    Catch ex As Exception
                        MessageBox.Show(ex.Message)
                    End Try
                End Using

                Return DeleteStatus
            Catch ex As Exception
                MessageBox.Show("In Function DeleteTableData - " & ex.Message)
                Return DeleteStatus
            End Try
        End Function

        Public Function WriteToXL(RequestType As String, FileName As String, SlopeData As DataTable) As Boolean
            Dim OutputCreated As Boolean = False
            Try

                CleanupXLInstances()
                Dim ExcelApp As Microsoft.Office.Interop.Excel.Application = New Microsoft.Office.Interop.Excel.Application()
                Dim xlWorkBook As Microsoft.Office.Interop.Excel.Workbook
                Dim xlWorkSheet As Microsoft.Office.Interop.Excel.Worksheet

                'If Not File.Exists(FileName) Then

                '    xlWorkBook = ExcelApp.Workbooks.Add()
                '    xlWorkSheet = CType(xlWorkBook.Sheets("Sheet1"), Microsoft.Office.Interop.Excel.Worksheet)
                '    xlWorkSheet.Name = "SlopeData"
                '    xlWorkBook.SaveAs(FileName)
                '    xlWorkBook.Close(True)
                'End If


                xlWorkBook = ExcelApp.Workbooks.Open(FileName)

                'xlWorkSheet = CType(xlWorkBook.Sheets(0), Microsoft.Office.Interop.Excel.Worksheet)
                xlWorkSheet = CType(xlWorkBook.Worksheets.Item(1), Microsoft.Office.Interop.Excel.Worksheet)


                Dim workSheetRange As Microsoft.Office.Interop.Excel.Range
                Dim RowCtr As Integer = 1


                workSheetRange = xlWorkSheet.UsedRange

                workSheetRange.ClearContents()

                Dim xlHeaderRange As Microsoft.Office.Interop.Excel.Range

                xlHeaderRange = xlWorkSheet.Range("A1", "H1")
                xlHeaderRange.Font.Bold = True

                If RequestType = "ContinuousSlope" Then

                    xlWorkSheet.Cells(1, 1) = "Max Weight"
                    xlWorkSheet.Cells(1, 2) = "Max Speed"
                    xlWorkSheet.Cells(1, 3) = "T Desc"
                    xlWorkSheet.Cells(1, 4) = "T Emerge"
                    xlWorkSheet.Cells(1, 5) = "T Final"
                    xlWorkSheet.Cells(1, 6) = "Time"

                    RowCtr = 2

                    For Each myRow In SlopeData.Rows

                        xlWorkSheet.Cells(RowCtr, 1) = myRow.Item(0)
                        xlWorkSheet.Cells(RowCtr, 2) = myRow.Item(1)
                        xlWorkSheet.Cells(RowCtr, 3) = myRow.Item(2)
                        xlWorkSheet.Cells(RowCtr, 4) = myRow.Item(3)
                        xlWorkSheet.Cells(RowCtr, 5) = myRow.Item(4)
                        xlWorkSheet.Cells(RowCtr, 6) = myRow.Item(5)

                        RowCtr = RowCtr + 1
                    Next

                    xlWorkSheet.Name = "Cont. Slope Data"

                    xlWorkSheet.Cells.NumberFormat = "0#"

                ElseIf RequestType = "SeparateSlope" Then
                    xlWorkSheet.Cells(1, 1) = "Number Grades"
                    xlWorkSheet.Cells(1, 2) = "Group Number"
                    xlWorkSheet.Cells(1, 3) = "Max Weight"
                    xlWorkSheet.Cells(1, 4) = "Max Speed"
                    xlWorkSheet.Cells(1, 5) = "T Desc"
                    xlWorkSheet.Cells(1, 6) = "T Emerge"
                    xlWorkSheet.Cells(1, 7) = "T Final"
                    xlWorkSheet.Cells(1, 8) = "Time"

                    RowCtr = 2

                    For Each myRow In SlopeData.Rows

                        xlWorkSheet.Cells(RowCtr, 1) = myRow.Item(0)
                        xlWorkSheet.Cells(RowCtr, 2) = myRow.Item(1)
                        xlWorkSheet.Cells(RowCtr, 3) = myRow.Item(2)
                        xlWorkSheet.Cells(RowCtr, 4) = myRow.Item(3)
                        xlWorkSheet.Cells(RowCtr, 5) = myRow.Item(4)
                        xlWorkSheet.Cells(RowCtr, 6) = myRow.Item(5)
                        xlWorkSheet.Cells(RowCtr, 7) = myRow.Item(6)
                        xlWorkSheet.Cells(RowCtr, 8) = myRow.Item(7)

                        RowCtr = RowCtr + 1
                    Next

                    xlWorkSheet.Name = "Sep. Slope Data"
                    xlWorkSheet.Cells.NumberFormat = "0#"

                ElseIf RequestType = "TempProfile" Then
                    xlWorkSheet.Name = "Temp Profile Data"

                    xlWorkSheet.Cells(1, 1) = "Weight"
                    xlWorkSheet.Cells(1, 2) = "Speed"
                    xlWorkSheet.Cells(1, 3) = "Distance"
                    xlWorkSheet.Cells(1, 4) = "Grade %"
                    xlWorkSheet.Cells(1, 5) = "T Desc"
                    xlWorkSheet.Cells(1, 6) = "T Emerge"
                    xlWorkSheet.Cells(1, 7) = "T Final"

                    RowCtr = 2

                    For Each myRow In SlopeData.Rows

                        xlWorkSheet.Cells(RowCtr, 1) = myRow.Item(0)
                        xlWorkSheet.Cells(RowCtr, 2) = myRow.Item(1)
                        xlWorkSheet.Cells(RowCtr, 3) = myRow.Item(2)
                        xlWorkSheet.Cells(RowCtr, 4).NumberFormat = "#0.0"
                        xlWorkSheet.Cells(RowCtr, 4) = myRow.Item(3)
                        xlWorkSheet.Cells(RowCtr, 4).NumberFormat = "#0.000"
                        xlWorkSheet.Cells(RowCtr, 5) = myRow.Item(4)
                        xlWorkSheet.Cells(RowCtr, 6) = myRow.Item(5)
                        xlWorkSheet.Cells(RowCtr, 7) = myRow.Item(6)
                        RowCtr = RowCtr + 1
                    Next

                End If

                xlWorkSheet.Columns("A:H").ColumnWidth = 50
                xlWorkSheet.Columns("A:H").Style.WrapText = True
                xlWorkSheet.Columns("A:H").AutoFit()



                xlWorkSheet.Cells.Style.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter
                xlWorkSheet.Cells.Style.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter

                xlWorkBook.Save()
                OutputCreated = True

                CleanupXLInstances()

                Return OutputCreated
            Catch ex As Exception
                MessageBox.Show("In Function WriteToXL - " & ex.Message)
                Return OutputCreated
            End Try
        End Function

        Public Sub CleanupXLInstances()
            Dim XLProcess As System.Diagnostics.Process = New System.Diagnostics.Process()
            Dim startInfo As System.Diagnostics.ProcessStartInfo = New System.Diagnostics.ProcessStartInfo()

            Try
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden
                startInfo.FileName = "cmd.exe"
                startInfo.Arguments = "/C taskkill.exe /F /IM excel.exe"
                XLProcess.StartInfo = startInfo
                XLProcess.Start()
                System.Threading.Thread.Sleep(500)

                XLProcess = Nothing
                startInfo = Nothing

            Catch ex As Exception
                MessageBox.Show("In Function CleanupXLInstances - " & ex.Message)
                XLProcess = Nothing
                startInfo = Nothing
            End Try


        End Sub
        Public Function IsValidText(InputText As String) As Boolean
            Dim ValidText As Boolean = True
            Try
                'Add new escape chars here to the pattern 
                Dim textPattern As String = "['%]"


                If Regex.IsMatch(InputText, textPattern) Then
                    ValidText = False
                End If
                Return ValidText
            Catch ex As Exception
                Return ValidText
            End Try
        End Function
    End Class

    Public Class SqlServerLoginResult
        Public Property Success() As Boolean
        Public ReadOnly Property Failed() As Boolean
            Get
                Return Success = False
            End Get
        End Property
        Public Property GenericException() As Boolean
        Public Property Message() As String

        Public Overrides Function ToString() As String
            Return Message
        End Function
    End Class


End Namespace
