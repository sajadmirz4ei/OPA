package example

default allow = false

allow {
    input.method == "GET"
    input.path == "/api/users"
    input.user != ""
}

allow {
    input.method == "POST"
    input.path == "/api/users"
    input.role == "admin"
}

allow {
    input.method == "POST"
    input.path == "/api/token"
}