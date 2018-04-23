@ModelType IEnumerable(Of StoredProcedures.Models.SelectSupportTeam_Result)
@Html.DevExpress().GridView(Sub(settings)

                                settings.Name = "supportGrid"
                                settings.CallbackRouteValues = New With {.Controller = "Home", .Action = "SupportGridPartial"}

                                settings.SettingsEditing.AddNewRowRouteValues = New With {.Controller = "Home", .Action = "SupportGridInsert"}
                                settings.SettingsEditing.DeleteRowRouteValues = New With {.Controller = "Home", .Action = "SupportGridDelete"}

                                settings.KeyFieldName = "Id"

                                settings.CommandColumn.Visible = True
                                settings.CommandColumn.NewButton.Visible = True
                                settings.CommandColumn.DeleteButton.Visible = True

                                settings.Columns.Add(Sub(column)
                                                         column.FieldName = "Id"
                                                         column.Width = 100

                                                         column.EditFormSettings.Visible = DefaultBoolean.False
                                                     End Sub)
                                settings.Columns.Add(Sub(column)
                                                         column.FieldName = "Name"
                                                         column.Width = 200

                                                         column.EditFormSettings.ColumnSpan = 2
                                                     End Sub)

                                settings.DataBound = Function(s, e)
                                                         If (TempData.ContainsKey("message")) Then
                                                             TryCast(s, MVCxGridView).JSProperties("cpMessage") = TempData("message")
                                                         End If
                                                     End Function

                                settings.SettingsLoadingPanel.Mode = GridViewLoadingPanelMode.ShowOnStatusBar
                                settings.Settings.ShowStatusBar = GridViewStatusBarMode.Visible

                                settings.SetStatusBarTemplateContent("<div id=""message"" style=""display: none;""></div>")

                                settings.ClientSideEvents.EndCallback = "grid_EndCallback"

                            End Sub).Bind(Model).GetHtml()
