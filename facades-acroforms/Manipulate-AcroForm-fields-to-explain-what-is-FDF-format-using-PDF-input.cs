using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source PDF, the exported FDF and the optional XML representation
        const string pdfPath = "input_form.pdf";
        const string fdfPath = "output.fdf";
        const string xmlPath = "output.xml";

        // Verify that the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // --------------------------------------------------------------------
        // 1. Load the PDF as an AcroForm using the Form facade.
        // --------------------------------------------------------------------
        // The Form class works directly with AcroForm fields.
        // It can be instantiated with the PDF file name.
        Form form = new Form(pdfPath);

        // --------------------------------------------------------------------
        // 2. Export the form field data to an FDF stream.
        // --------------------------------------------------------------------
        // FDF (Forms Data Format) is a lightweight file format that contains
        // only the values of the form fields, not the full PDF content.
        // It is useful for transmitting form data or for importing it into
        // another PDF.
        using (FileStream fdfStream = new FileStream(fdfPath, FileMode.Create, FileAccess.Write))
        {
            form.ExportFdf(fdfStream);
        }

        Console.WriteLine($"Form data exported to FDF: {fdfPath}");

        // --------------------------------------------------------------------
        // 3. (Optional) Convert the FDF file to an XML representation.
        // --------------------------------------------------------------------
        // The FormDataConverter class provides static methods to transform
        // between XML and FDF. Converting to XML makes the data human‑readable.
        if (File.Exists(fdfPath))
        {
            using (FileStream fdfRead = new FileStream(fdfPath, FileMode.Open, FileAccess.Read))
            using (FileStream xmlWrite = new FileStream(xmlPath, FileMode.Create, FileAccess.Write))
            {
                FormDataConverter.ConvertFdfToXml(fdfRead, xmlWrite);
            }

            Console.WriteLine($"FDF converted to XML: {xmlPath}");
        }

        // Clean up the Form facade
        form.Close();
    }
}