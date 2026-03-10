using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "sample.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // -----------------------------------------------------------------
        // Retrieve metadata and page information using PdfFileInfo (Facade)
        // -----------------------------------------------------------------
        using (PdfFileInfo fileInfo = new PdfFileInfo(inputPdf))
        {
            // Standard document metadata
            Console.WriteLine($"Title          : {fileInfo.Title}");
            Console.WriteLine($"Author         : {fileInfo.Author}");
            Console.WriteLine($"Subject        : {fileInfo.Subject}");
            Console.WriteLine($"Keywords       : {fileInfo.Keywords}");
            Console.WriteLine($"Creator        : {fileInfo.Creator}");
            Console.WriteLine($"Producer       : {fileInfo.Producer}");
            Console.WriteLine($"CreationDate   : {fileInfo.CreationDate}");
            Console.WriteLine($"ModDate        : {fileInfo.ModDate}");

            // Security / encryption information
            Console.WriteLine($"IsEncrypted    : {fileInfo.IsEncrypted}");
            Console.WriteLine($"HasOpenPassword: {fileInfo.HasOpenPassword}");
            Console.WriteLine($"HasEditPassword: {fileInfo.HasEditPassword}");

            // Page count and PDF version
            Console.WriteLine($"Number of Pages: {fileInfo.NumberOfPages}");
            Console.WriteLine($"PDF Version    : {fileInfo.GetPdfVersion()}");

            // Detailed per‑page geometry
            for (int pageNum = 1; pageNum <= fileInfo.NumberOfPages; pageNum++)
            {
                double width   = fileInfo.GetPageWidth(pageNum);
                double height  = fileInfo.GetPageHeight(pageNum);
                int    rotation = fileInfo.GetPageRotation(pageNum);
                double xOffset = fileInfo.GetPageXOffset(pageNum);
                double yOffset = fileInfo.GetPageYOffset(pageNum);

                Console.WriteLine(
                    $"Page {pageNum,2}: Width={width,8:F2}  Height={height,8:F2}  " +
                    $"Rotation={rotation,2}  XOffset={xOffset,6:F2}  YOffset={yOffset,6:F2}");
            }

            // Document privilege settings (print, edit, etc.)
            var privilege = fileInfo.GetDocumentPrivilege();
            bool canPrint   = GetPrivilegeFlag(privilege, "PrintAllowed", "IsPrintAllowed");
            bool canModify  = GetPrivilegeFlag(privilege, "ModifyAllowed", "IsModifyAllowed");
            bool canCopy    = GetPrivilegeFlag(privilege, "CopyAllowed", "IsCopyAllowed");
            bool canAnnot   = GetPrivilegeFlag(privilege, "AddOrModifyAnnotationsAllowed", "IsAddOrModifyAnnotationsAllowed");

            Console.WriteLine($"CanPrint       : {canPrint}");
            Console.WriteLine($"CanModify      : {canModify}");
            Console.WriteLine($"CanCopy        : {canCopy}");
            Console.WriteLine($"CanAddOrModifyAnnotations: {canAnnot}");
        }

        // -----------------------------------------------------------------
        // Alternative way to obtain page count using PdfViewer (Facade)
        // -----------------------------------------------------------------
        PdfViewer viewer = new PdfViewer();
        try
        {
            viewer.BindPdf(inputPdf);
            Console.WriteLine($"Page count via PdfViewer: {viewer.PageCount}");
        }
        finally
        {
            viewer.Close(); // PdfViewer does not implement IDisposable
        }
    }

    /// <summary>
    /// Retrieves a boolean privilege flag using reflection. It first tries to read a
    /// property with the name <paramref name="primaryName"/>; if that property does
    /// not exist it falls back to a method named <paramref name="fallbackName"/>.
    /// This approach works across multiple Aspose.Pdf versions where the API name
    /// may have changed.
    /// </summary>
    private static bool GetPrivilegeFlag(object privilegeObj, string primaryName, string fallbackName)
    {
        if (privilegeObj == null) return false;
        Type t = privilegeObj.GetType();

        // Try property first (e.g., PrintAllowed)
        PropertyInfo prop = t.GetProperty(primaryName, BindingFlags.Public | BindingFlags.Instance);
        if (prop != null && prop.PropertyType == typeof(bool))
        {
            return (bool)prop.GetValue(privilegeObj);
        }

        // If property not found, try a parameter‑less method (e.g., IsPrintAllowed())
        MethodInfo method = t.GetMethod(fallbackName, BindingFlags.Public | BindingFlags.Instance, null, Type.EmptyTypes, null);
        if (method != null && method.ReturnType == typeof(bool))
        {
            return (bool)method.Invoke(privilegeObj, null);
        }

        // Default when neither member exists
        return false;
    }
}
