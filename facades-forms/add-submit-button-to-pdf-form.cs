using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source PDF and the resulting PDF with the submit button
        const string srcPdf  = "input.pdf";
        const string dstPdf  = "output_with_submit.pdf";

        // Ensure the source file exists
        if (!System.IO.File.Exists(srcPdf))
        {
            Console.Error.WriteLine($"Source file not found: {srcPdf}");
            return;
        }

        // FormEditor works with a source PDF and a destination PDF.
        // The constructor takes the input file and the output file paths.
        using (FormEditor formEditor = new FormEditor(srcPdf, dstPdf))
        {
            // Add a submit button named "SubmitForm" on page 1.
            // Parameters: fieldName, pageNumber, label, URL, llx, lly, urx, ury
            formEditor.AddSubmitBtn(
                fieldName: "SubmitForm",
                page: 1,
                label: "Submit",
                url: "https://api.example.com/submit",
                llx: 100f,
                lly: 200f,
                urx: 200f,
                ury: 250f);

            // Save writes the changes to the destination file specified in the constructor.
            formEditor.Save();
        }

        Console.WriteLine($"Submit button added. Output saved to '{dstPdf}'.");
    }
}