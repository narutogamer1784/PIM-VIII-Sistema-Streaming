package com.example.pimuserapp;

import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.widget.Toast;
import androidx.activity.EdgeToEdge;
import androidx.appcompat.app.AppCompatActivity;
import androidx.core.graphics.Insets;
import androidx.core.view.ViewCompat;
import androidx.core.view.WindowInsetsCompat;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import java.util.List;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class MainActivity extends AppCompatActivity {

    private RecyclerView recyclerView;
    private PlaylistAdapter adapter;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        EdgeToEdge.enable(this);
        setContentView(R.layout.activity_main);

        ViewCompat.setOnApplyWindowInsetsListener(findViewById(R.id.recyclerViewPlaylists), (v, insets) -> {
            Insets systemBars = insets.getInsets(WindowInsetsCompat.Type.systemBars());
            v.setPadding(systemBars.left, systemBars.top, systemBars.right, systemBars.bottom);
            return insets;
        });

        recyclerView = findViewById(R.id.recyclerViewPlaylists);
        recyclerView.setLayoutManager(new LinearLayoutManager(this));

        Retrofit retrofit = new Retrofit.Builder()
                .baseUrl("http://10.0.2.2:5207/")
                .addConverterFactory(GsonConverterFactory.create())
                .build();

        ApiService service = retrofit.create(ApiService.class);

        service.getPlaylists().enqueue(new Callback<List<Playlist>>() {
            @Override
            public void onResponse(Call<List<Playlist>> call, Response<List<Playlist>> response) {
                if (response.isSuccessful() && response.body() != null) {
                    List<Playlist> playlists = response.body();

                    adapter = new PlaylistAdapter(playlists, playlist -> {
                        Intent intent = new Intent(MainActivity.this, DetailActivity.class);
                        intent.putExtra("EXTRA_TITLE", playlist.title);
                        intent.putExtra("EXTRA_DESC", playlist.description);


                        StringBuilder songsBuilder = new StringBuilder();
                        if (playlist.items != null && !playlist.items.isEmpty()) {
                            for (PlaylistItem item : playlist.items) {
                                if (item.content != null) {
                                    // Monta uma linha: "🎵 Evidências (Musica)"
                                    songsBuilder.append("🎵 ")
                                            .append(item.content.title)
                                            .append(" (")
                                            .append(item.content.type)
                                            .append(")\n");
                                }
                            }
                        } else {
                            songsBuilder.append("Nenhum conteúdo disponível.");
                        }
                        // Coloca o listão na mala
                        intent.putExtra("EXTRA_SONGS", songsBuilder.toString());


                        startActivity(intent);
                    });

                    recyclerView.setAdapter(adapter);

                } else {
                    Toast.makeText(MainActivity.this, "Erro no servidor: " + response.code(), Toast.LENGTH_LONG).show();
                }
            }

            @Override
            public void onFailure(Call<List<Playlist>> call, Throwable t) {
                Toast.makeText(MainActivity.this, "Falha na conexão: " + t.getMessage(), Toast.LENGTH_LONG).show();
                Log.e("COWBOY_FAIL", t.getMessage());
            }
        });
    }
}