#include "Application.h"

int main(){
    auto app = new Application(800, 600);
    app->Init();
    delete app;
    return 0;
}
