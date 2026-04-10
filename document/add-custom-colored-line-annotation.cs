// Program.cs

using System;

using System.IO;

using Aspose.Pdf;

using Aspose.Pdf.Annotations;



class Program

{

    static void Main()

    {

        const string inputPath = "input.pdf";

        const string outputPath = "output_with_line.pdf";



        if (!File.Exists(inputPath))

        {

            Console.Error.WriteLine($"File not found: {inputPath}");

            return;

        }



        // Load the PDF document (using the lifecycle rule for disposal)

        using (Document doc = new Document(inputPath))

        {

            // Get the first page (1‑based indexing)

            Page page = doc.Pages[1];



            // Define the annotation rectangle (the area that contains the line)

            // Adjust coordinates as needed to cover the desired text region

            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);



            // Define start and end points of the line

            Aspose.Pdf.Point start = new Aspose.Pdf.Point(110, 510);

            Aspose.Pdf.Point end   = new Aspose.Pdf.Point(290, 510);



            // Create the line annotation on the page

            LineAnnotation line = new LineAnnotation(page, rect, start, end);



            // Set custom color (blue) and thickness (3 points)

            // Color.FromRgb expects double values in the range 0‑1

            line.Color = Aspose.Pdf.Color.FromRgb(0.0, 0.0, 1.0);

            line.Border = new Border(line) { Width = 3 };



            // Add the annotation to the page

            page.Annotations.Add(line);



            // Save the modified PDF

            doc.Save(outputPath);

        }



        Console.WriteLine($"Line annotation added and saved to '{outputPath}'.");

    }

}



// AsposePdfApi.GlobalUsings.g.cs (empty placeholder to satisfy the project build)

// This file is intentionally left blank. It resolves the CS2001 error caused by a missing generated global usings file.

