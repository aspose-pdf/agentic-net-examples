using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf;

class Program
{
    // Usage: AddAttachmentAndViewerPrefs.exe <inputPdf> <attachmentFile> <outputPdf>
    static void Main(string[] args)
    {
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <inputPdf> <attachmentFile> <outputPdf>");
            return;
        }

        string inputPdfPath   = args[0];
        string attachmentPath = args[1];
        string outputPdfPath  = args[2];

        // Validate files
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
            return;
        }

        try
        {
            // Load the PDF document (Aspose.Pdf.Document, not PdfFileEditor)
            Document pdfDoc = new Document(inputPdfPath);

            // ------------------------------------------------------------
            // 1. Add attachment (using EmbeddedFiles collection)
            // ------------------------------------------------------------
            // Create a FileSpecification for the attachment. The Contents
            // property must contain the file data as a stream.
            var fileSpec = new FileSpecification(Path.GetFileName(attachmentPath));
            fileSpec.Contents = new MemoryStream(File.ReadAllBytes(attachmentPath));
            pdfDoc.EmbeddedFiles.Add(fileSpec);

            // ------------------------------------------------------------
            // 2. Set viewer preferences (if the API version supports it)
            // ------------------------------------------------------------
            SetViewerPreferencesIfSupported(pdfDoc);

            // Save the modified PDF to the output path.
            pdfDoc.Save(outputPdfPath);

            Console.WriteLine($"Attachment '{Path.GetFileName(attachmentPath)}' added and viewer preferences set (if supported).");
            Console.WriteLine($"Output saved to: {outputPdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // --------------------------------------------------------------------
    // Helper: tries to set viewer preferences via reflection.
    // --------------------------------------------------------------------
    private static void SetViewerPreferencesIfSupported(Document doc)
    {
        // Look for a property named "ViewerPreferences" on the Document class.
        PropertyInfo vpProp = typeof(Document).GetProperty("ViewerPreferences", BindingFlags.Public | BindingFlags.Instance);
        if (vpProp == null)
        {
            // The running Aspose.Pdf version does not expose ViewerPreferences – nothing to do.
            return;
        }

        object vpInstance = vpProp.GetValue(doc);
        if (vpInstance == null)
        {
            // Some versions expose the property but require an instance to be created.
            // Attempt to create one via the default constructor.
            Type vpType = vpProp.PropertyType;
            ConstructorInfo ctor = vpType.GetConstructor(Type.EmptyTypes);
            if (ctor != null)
            {
                vpInstance = ctor.Invoke(null);
                vpProp.SetValue(doc, vpInstance);
            }
            else
            {
                // Cannot create an instance – abort setting preferences.
                return;
            }
        }

        // Helper to set a boolean property if it exists.
        void SetBool(string name, bool value)
        {
            PropertyInfo pi = vpInstance.GetType().GetProperty(name, BindingFlags.Public | BindingFlags.Instance);
            if (pi != null && pi.CanWrite && pi.PropertyType == typeof(bool))
            {
                pi.SetValue(vpInstance, value);
            }
        }

        SetBool("HideToolbar", true);
        SetBool("HideMenubar", true);
        SetBool("FitWindow", true);
        SetBool("CenterWindow", true);
        SetBool("DisplayDocTitle", true);
    }
}
