pipeline {
    agent any

    environment {
        DB_CONNECTION = credentials('DB_CONNECTION_STRING')
        DOCKER_IMAGE = "open-expense:latest"
        REGISTRY_SERVER = "registry.opeloooco.uk"
        REG_AUTH = credentials('REGISTRY_AUTH')
    }

    stages {
        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        // stage('Test') {
        //     environment {
        //         ConnectionStrings__DefaultConnection = "${DB_CONNECTION}"
        //     }
        //     steps {
        //         // Testing menggunakan SDK di server Jenkins sebelum masuk Docker
        //         sh 'dotnet test --verbosity normal'
        //     }
        // }

        stage('Docker Build') {
            steps {
                script {
                    sh "echo ${REG_AUTH_PSW} | docker login ${REGISTRY} -u ${REG_AUTH_USR} --password-stdin"
                    // Membangun image menggunakan Dockerfile Alpine tadi
                    sh "docker build -t ${REGISTRY_SERVER}/${DOCKER_IMAGE} ."
                    // push ke registry personal
                    sh "docker push ${REGISTRY}/${IMAGE_NAME}:latest"
                }
            }
        }

        stage('Docker Run/Deploy') {
            steps {
                script {
                    // Stop container lama jika ada
                    sh "docker stop open-expense || true"
                    sh "docker rm open-expense || true"
                    
                    // Jalankan container baru dengan koneksi DB dari Jenkins
                    sh """
                        docker run -d \
                        --name open-expense \
                        -p 3001:8080 \
                        -e ConnectionStrings__DefaultConnection='${DB_CONNECTION}' \
                        -e ASPNETCORE_ENVIRONMENT=Production \
                        ${DOCKER_IMAGE}
                    """
                }
            }
        }
    }

    post {
        always {
            // Kita tidak cleanWs() jika ingin docker cache tetap ada untuk build berikutnya
            echo 'Pipeline finished.'
        }
    }
}
