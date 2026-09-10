using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string jsonLog  = "annotations_log.json";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Initialize the annotation editor facade
            using (PdfAnnotationEditor annotEditor = new PdfAnnotationEditor())
            {
                annotEditor.BindPdf(doc);

                // Export form fields (widget annotations) to JSON using the Form facade
                using (Form form = new Form(doc))
                {
                    using (FileStream jsonStream = new FileStream(jsonLog, FileMode.Create, FileAccess.Write))
                    {
                        // Export all form fields to JSON; indented for readability
                        form.ExportJson(jsonStream, indented: true);
                    }
                }

                // Optional: export all annotations to XFDF (XML) if needed
                // using (FileStream xfdfStream = new FileStream("annotations.xfdf", FileMode.Create, FileAccess.Write))
                // {
                //     annotEditor.ExportAnnotationsToXfdf(xfdfStream);
                // }
            }

            // No modifications are made to the PDF, so no Save() call is required here.
        }

        Console.WriteLine($"Annotation data exported to JSON log: {jsonLog}");
    }
}