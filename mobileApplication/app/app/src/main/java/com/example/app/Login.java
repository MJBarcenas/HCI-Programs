package com.example.app;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.os.Handler;
import android.os.Looper;
import android.view.View;
import android.widget.Button;
import android.widget.Toast;

import com.google.android.material.textfield.TextInputEditText;
import com.vishnusivadas.advanced_httpurlconnection.PutData;

import java.util.Arrays;

public class Login extends AppCompatActivity {

    TextInputEditText txtInptStdntNum, txtInptAccpss;
    Button btnLgin;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);

        txtInptStdntNum = findViewById(R.id.studentnum);
        txtInptAccpss = findViewById(R.id.accpass);
        btnLgin = findViewById(R.id.loginbtn);

        btnLgin.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {

                String studentnum, accpass;
                studentnum = String.valueOf(txtInptStdntNum.getText());
                accpass = String.valueOf(txtInptAccpss.getText());

                if (!studentnum.equals("") && !accpass.equals("")) {
                    Handler handler = new Handler();
                    handler.post(new Runnable() {
                        @Override
                        public void run() {
                            //Starting Write and Read data with URL
                            //Creating array for parameters
                            String[] field = new String[2];
                            field[0] = "studentnum";
                            field[1] = "accpass";
                            //Creating array for data
                            String[] data = new String[2];
                            data[0] = studentnum;
                            data[1] = accpass;

                            PutData putData = new PutData("http://192.168.1.52/programs/mobileApplication/Login/login.php", "POST", field, data);
                            if (putData.startPut()) {
                                if (putData.onComplete()) {
                                    String result = putData.getResult();
                                    String[] results = result.split(",");

                                    if (results.length > 1 && results[7].equals("Login Success")) {
                                        Toast.makeText(getApplicationContext(),results[7],Toast.LENGTH_SHORT).show();

                                        Intent intent = new Intent(getApplicationContext(), MainActivity.class);
                                        intent.putExtra("param", Arrays.copyOfRange(results, 0, 7));

                                        //Bundle param = new Bundle();

                                        //param.putStringArray("param", results);

                                        //intent.putExtras(param);

                                        startActivity(intent);
                                        finish();
                                    } else {
                                        Toast.makeText(getApplicationContext(),result,Toast.LENGTH_SHORT).show();
                                    }
                                }
                            }
                            //End Write and Read data with URL
                        }
                    });
                } else {
                    Toast.makeText(getApplicationContext(), "All fields are required", Toast.LENGTH_SHORT).show();
                }
            }
        });
    }
}