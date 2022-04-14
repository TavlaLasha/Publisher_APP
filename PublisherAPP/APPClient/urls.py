from os import stat
from django.conf import settings
from django.urls import path
from . import views
from django.conf.urls.static import static
from django.contrib.auth import views as auth_views

urlpatterns = [
    path('', views.index, name='index'),
    path('index', views.index, name='index'),
    
]