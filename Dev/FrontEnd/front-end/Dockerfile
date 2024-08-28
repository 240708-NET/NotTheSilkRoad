FROM node:alpine AS build
WORKDIR /front-end
COPY . .
RUN npm ci
ENTRYPOINT [ "npm", "run", "dev" ] 
# EXPOSE 80

# # Dockerfile for frontend