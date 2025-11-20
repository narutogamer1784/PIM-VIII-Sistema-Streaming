package com.example.pimuserapp;

import java.util.List;
import retrofit2.Call;
import retrofit2.http.GET;

public interface ApiService {

    @GET("api/Playlists")
    Call<List<Playlist>> getPlaylists();
}