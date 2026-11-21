using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the XFDF files that need to be merged
        string[] xfdfFiles = { "annotations1.xfdf", "annotations2.xfdf", "annotations3.xfdf" };
        // Input PDF to which the merged XFDF will be applied
        const string pdfInputPath = "input.pdf";
        // Output PDF after importing the merged XFDF
        const string pdfOutputPath = "output.pdf";
        // Temporary file that will hold the merged XFDF
        const string mergedXfdfPath = "merged.xfdf";

        // Verify that all source files exist
        foreach (var file in xfdfFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"XFDF file not found: {file}");
                return;
            }
        }
        if (!File.Exists(pdfInputPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfInputPath}");
            return;
        }

        // ------------------------------------------------------------
        // Merge the XFDF files into a single XFDF document
        // ------------------------------------------------------------
        // Create an empty XFDF root element with the required namespace
        XNamespace ns = "http://ns.adobe.com/xfdf/";
        XDocument mergedXfdf = new XDocument(new XElement(ns + "xfdf"));

        foreach (var xfdfPath in xfdfFiles)
        {
            // Load each XFDF file as XML
            XDocument doc = XDocument.Load(xfdfPath);
            XElement root = doc.Root;
            if (root != null)
            {
                // Append a copy of each child element (fields, annots, etc.) to the merged root
                foreach (var element in root.Elements())
                {
                    mergedXfdf.Root.Add(new XElement(element));
                }
            }
        }

        // Save the merged XFDF to a temporary file
        mergedXfdf.Save(mergedXfdfPath);

        // ------------------------------------------------------------
        // Import the merged XFDF into the PDF using the Form facade
        // ------------------------------------------------------------
        try
        {
            // Load the source PDF
            using (Document pdfDoc = new Document(pdfInputPath))
            using (Form formFacade = new Form(pdfDoc))
            {
                // Import the merged XFDF annotations (the method expects a Stream)
                using (FileStream xfdfStream = File.OpenRead(mergedXfdfPath))
                {
                    formFacade.ImportXfdf(xfdfStream);
                }

                // Save the resulting PDF with the imported annotations
                formFacade.Save(pdfOutputPath);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during XFDF import: {ex.Message}");
            return;
        }
        finally
        {
            // Clean up the temporary merged XFDF file
            if (File.Exists(mergedXfdfPath))
            {
                try { File.Delete(mergedXfdfPath); } catch { /* ignore cleanup errors */ }
            }
        }

        Console.WriteLine($"Merged XFDF imported successfully. Output PDF saved to '{pdfOutputPath}'.");
    }
}
