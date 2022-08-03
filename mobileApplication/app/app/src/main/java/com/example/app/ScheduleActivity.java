package com.example.app;

import androidx.appcompat.app.AppCompatActivity;

import android.net.Uri;
import android.os.Bundle;
import android.os.Environment;
import android.view.View;

import com.github.barteksc.pdfviewer.PDFView;

import java.io.File;

public class ScheduleActivity extends AppCompatActivity {

    PDFView schedule;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_schedule);

        schedule = findViewById(R.id.schedule);

        ViewPDF();
    }

    public void ViewPDF() {
        File schedFile = new File(Environment.getExternalStorageDirectory()
                + "/PDFs/" + "sched.pdf");

        Uri schedpath = Uri.fromFile(schedFile);

        schedule.fromUri(schedpath).load();

    }
}