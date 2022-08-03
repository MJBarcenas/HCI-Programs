package com.example.app;

import android.content.Context;
import android.os.AsyncTask;
import android.os.Environment;

import java.io.File;
import java.io.IOException;

public class DownloadTXT extends AsyncTask<String, Void, Void> {

    Context ctx;
    @Override
    protected Void doInBackground(String... strings) {

        String fileUrl = strings[0];
        String fileName = strings[1];

        String extStrorageDirectory = Environment.getExternalStorageDirectory().toString();

        File folder = new File(extStrorageDirectory + File.separator + "TXTs");
        folder.mkdir();

        File pdfFile = new File(folder + File.separator + fileName);

        try {
            pdfFile.createNewFile();

        } catch (IOException e) {
            e.printStackTrace();
        }

        FileDownloader.downloadFile(fileUrl, pdfFile);

        return null;

    }
}

