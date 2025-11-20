package com.example.pimuserapp;

import android.os.Bundle;
import android.widget.TextView;
import androidx.appcompat.app.AppCompatActivity;

public class DetailActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_detail);

        TextView titleView = findViewById(R.id.detailTitle);
        TextView descView = findViewById(R.id.detailDesc);

        // 1. ACHA O TEXTVIEW NOVO QUE CRIAMOS NO XML
        TextView songsView = findViewById(R.id.detailSongs);

        // 2. PEGA OS DADOS DA MALA (INTENT)
        String title = getIntent().getStringExtra("EXTRA_TITLE");
        String desc = getIntent().getStringExtra("EXTRA_DESC");

        // PEGA A LISTA DE MÚSICAS
        String songs = getIntent().getStringExtra("EXTRA_SONGS");

        // 3. JOGA NA TELA
        if (title != null) titleView.setText(title);
        if (desc != null) descView.setText(desc);

        // SE TIVER MÚSICA, MOSTRA. SE NÃO, AVISA.
        if (songs != null) {
            songsView.setText(songs);
        } else {
            songsView.setText("Erro ao carregar lista.");
        }
    }
}