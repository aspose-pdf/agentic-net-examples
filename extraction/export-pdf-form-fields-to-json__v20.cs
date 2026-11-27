using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Input PDF path (replace with your actual file)
        const string pdfPath = "input.pdf";

        // Output JSON file that will contain the exported form fields
        const string jsonOutputPath = "form_fields.json";

        // If the PDF is password‑protected, provide the user password here.
        // Leave empty ("") if no password is required.
        const string userPassword = "myPassword";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF document. The constructor overload with a password
            // opens encrypted PDFs without throwing an exception.
            using (Document doc = string.IsNullOrEmpty(userPassword)
                                   ? new Document(pdfPath)
                                   : new Document(pdfPath, userPassword))
            {
                // Some Aspose.Pdf versions expose the ExportPasswordValue flag
                // via a property on the Form object.  To keep the code compatible
                // with versions that do not have this property we set it via
                // reflection – if the property exists we enable it, otherwise we
                // simply continue (the default behaviour is to omit password
                // values).
                PropertyInfo exportPwdProp = doc.Form?.GetType()
                    .GetProperty("ExportPasswordValue", BindingFlags.Public | BindingFlags.Instance);
                if (exportPwdProp != null && exportPwdProp.CanWrite)
                {
                    exportPwdProp.SetValue(doc.Form, true);
                }

                // Export all form fields to JSON.  The ExportToJson method writes
                // the JSON representation of the AcroForm to a stream.
                using (FileStream jsonStream = new FileStream(jsonOutputPath, FileMode.Create, FileAccess.Write))
                {
                    doc.Form.ExportToJson(jsonStream);
                }

                Console.WriteLine($"Form fields exported to '{jsonOutputPath}'.");
            }
        }
        catch (InvalidPasswordException)
        {
            Console.Error.WriteLine("The provided password is incorrect.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
