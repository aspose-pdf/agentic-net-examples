using System;
using System.IO;
using System.Net.NetworkInformation;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the source PDF file
        const string pdfPath = @"C:\Input\sample.pdf";

        // Primary UNC path to the network share where images will be saved
        const string primaryUncFolder = @"\\Server\Share\Images";

        // Fallback local folder (used when the UNC share is unavailable)
        string fallbackFolder = Path.Combine(Path.GetTempPath(), "ExtractedImages");

        // Verify that the PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Resolve the destination folder – try the UNC share first, otherwise use the fallback
        string destFolder = ResolveDestinationFolder(primaryUncFolder, fallbackFolder);

        // Initialize the PdfExtractor facade
        PdfExtractor extractor = new PdfExtractor();

        // Bind the PDF document to the extractor
        extractor.BindPdf(pdfPath);

        // Extract images from the bound PDF
        extractor.ExtractImage();

        int imageIndex = 1;
        // Iterate through all extracted images
        while (extractor.HasNextImage())
        {
            // Build a file name for each image (saved as JPEG by default)
            string destFile = Path.Combine(destFolder, $"image-{imageIndex}.jpg");

            // Save the current image to the resolved path
            extractor.GetNextImage(destFile);

            imageIndex++;
        }

        Console.WriteLine($"Extracted {imageIndex - 1} image(s) to \"{destFolder}\".");
    }

    /// <summary>
    /// Returns a writable folder path. Tries the primary UNC folder first; if it cannot be accessed,
    /// creates and returns a fallback local folder.
    /// </summary>
    private static string ResolveDestinationFolder(string primaryUnc, string fallbackLocal)
    {
        try
        {
            // Quick reachability test – ping the server part of the UNC path
            string serverName = GetServerNameFromUnc(primaryUnc);
            if (!string.IsNullOrEmpty(serverName) && PingHost(serverName))
            {
                // Ensure the UNC directory exists (or create it)
                System.IO.Directory.CreateDirectory(primaryUnc);
                return primaryUnc;
            }
        }
        catch (IOException)
        {
            // Swallow – we'll fall back to the local folder
        }
        catch (UnauthorizedAccessException)
        {
            // Swallow – we'll fall back to the local folder
        }

        // If we reach here, use the fallback local folder
        System.IO.Directory.CreateDirectory(fallbackLocal);
        return fallbackLocal;
    }

    /// <summary>
    /// Extracts the server name from a UNC path (e.g., "\\Server\Share" => "Server").
    /// </summary>
    private static string GetServerNameFromUnc(string uncPath)
    {
        if (string.IsNullOrWhiteSpace(uncPath))
            return string.Empty;

        // UNC format: \\ServerName\ShareName\Optional\Path
        // Split on backslash, ignoring empty entries caused by the leading \\.
        var parts = uncPath.Split(new[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
        return parts.Length > 0 ? parts[0] : string.Empty;
    }

    /// <summary>
    /// Sends a single ICMP echo request to verify that the host is reachable.
    /// Returns true if a reply is received within the timeout.
    /// </summary>
    private static bool PingHost(string host)
    {
        try
        {
            using (var ping = new Ping())
            {
                var reply = ping.Send(host, 1000);
                return reply != null && reply.Status == IPStatus.Success;
            }
        }
        catch
        {
            // Any exception (e.g., Ping not allowed) is treated as unreachable.
            return false;
        }
    }
}
