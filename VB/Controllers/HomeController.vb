Imports System.Linq
Imports System.Web.Mvc
Imports StoredProcedures.Models
Imports DevExpress.Web.Mvc

Namespace StoredProcedures.Controllers

    Public Class HomeController
        Inherits Controller

        Public Function Index() As ActionResult
            ViewBag.Message = "The example demonstrates how to bind the grid to a Stored Procedure result.<br /> All changes that are cleared after 10 minutes."
            ' Clean 10-min old records 
            Dim context As StoredProcedureEntities = New StoredProcedureEntities()
            Dim oldDate As Date = Date.Now.AddMinutes(-10.0)
            Dim supportTeamEntities = context.SupportTeams.OrderBy(Function(i) i.Id).Skip(4).Where(Function(i) i.ChangeDate < oldDate).ToList()
            For Each item As SupportTeam In supportTeamEntities
                context.DeleteObject(item)
            Next

            context.SaveChanges()
            Return View()
        End Function

        Public Function SupportGridPartial() As ActionResult
            Dim context As StoredProcedureEntities = New StoredProcedureEntities()
            Dim model = context.SelectSupportTeam()
            Return PartialView("SupportGridPartial", model)
        End Function

        <HttpPost>
        Public Function SupportGridInsert(<ModelBinder(GetType(DevExpressEditorsBinder))> ByVal supportEngineer As SelectSupportTeam_Result) As ActionResult
            Dim context As StoredProcedureEntities = New StoredProcedureEntities()
            Dim result = context.InsertSupportEngineer(supportEngineer.Name).First() ' inserted key value
            If result.HasValue Then TempData("message") = "Inserted row key: " & result.Value.ToString()
            Dim model = context.SelectSupportTeam()
            Return PartialView("SupportGridPartial", model)
        End Function

        <HttpPost>
        Public Function SupportGridDelete(ByVal Id As Integer) As ActionResult
            Dim context As StoredProcedureEntities = New StoredProcedureEntities()
            ' it is not allowed to delete some initial records 
            Dim foundItem As Integer = context.SupportTeams.Take(4).Where(Function(i) i.Id = Id).Count()
            If foundItem <> 0 Then
                TempData("message") = "Error: It is not possible to delete test records"
            Else
                Dim result = context.DeleteSupportEngineer(Id).First() ' a number of deleted rows (usually 1)
                If result.HasValue Then TempData("message") = "A number of deleted rows: " & result.Value.ToString()
            End If

            Dim model = context.SelectSupportTeam()
            Return PartialView("SupportGridPartial", model)
        End Function
    End Class
End Namespace
