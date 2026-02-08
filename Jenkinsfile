pipeline {
    agent any

    environment {
        DB_CONNECTION = credentials('DB_CONNECTION_STRING')
        DOCKER_IMAGE_NAME = "open-expense"
        DOCKER_TAG = "latest"
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
                    // Gunakan single quotes '' agar password tidak bocor di log
                    // Gunakan REGISTRY_SERVER (sesuai environment di atas)
                    sh 'echo ${REG_AUTH_PSW} | docker login ${REGISTRY_SERVER} -u ${REG_AUTH_USR} --password-stdin'
                    
                    // Build dengan format: registry.opeloooco.uk/open-expense:latest
                    sh "docker build -t ${REGISTRY_SERVER}/${DOCKER_IMAGE_NAME}:${DOCKER_TAG} ."
                    
                    // Push ke registry
                    sh "docker push ${REGISTRY_SERVER}/${DOCKER_IMAGE_NAME}:${DOCKER_TAG}"
                }
            }
        }

        stage('Docker Run/Deploy') {
            steps {
                script {
                    sh "docker stop open-expense || true"
                    sh "docker rm open-expense || true"
                    
                    // Jalankan container menggunakan image yang baru di-push
                    sh """
                        docker run -d \
                        --name open-expense \
                        -p 3001:8080 \
                        -e ConnectionStrings__DefaultConnection='${DB_CONNECTION}' \
                        -e ASPNETCORE_ENVIRONMENT=Production \
                        ${REGISTRY_SERVER}/${DOCKER_IMAGE_NAME}:${DOCKER_TAG}
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
